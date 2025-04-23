using System;
using System.Linq;
using OpenRpg.Combat.Abilities;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Extensions;
using OpenRpg.Combat.Processors.Attacks.Entity;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Utils;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Fantasy.Extensions;

namespace OpenRpg.Genres.Fantasy.Combat
{
    public class FantasyAttackGenerator : IEntityAttackGenerator
    {
        public float DamageVariance { get; set; } = 0.05f;
        public IRandomizer Randomizer { get; set; }
        
        public FantasyAttackGenerator(IRandomizer randomizer)
        { Randomizer = randomizer; }

        public Damage ApplyVariance(Damage damage)
        {
            if(damage.Value == 0) { return damage; }
            
            var varianceAmount = damage.Value * DamageVariance;
            var randomVariance = Randomizer.Random(-varianceAmount, varianceAmount);
            damage.Value += randomVariance;
            return damage;
        }

        public virtual (float scaledCritRate, float scaledCritMultiplier) GetScaledCriticalRateAndMultiplier(
            EntityStatsVariables stats)
        {
            return (stats.CriticalDamageChance(), stats.CriticalDamageMultiplier());
        }

        public (bool DidCrit, Damage[] Damages) AttemptToCritical(Damage[] damages, EntityStatsVariables stats)
        {
            if(damages?.Length == 0) { return (false, Array.Empty<Damage>()); }
            
            var scaledCritData = GetScaledCriticalRateAndMultiplier(stats);
            var isCritical = Randomizer.ShouldCritical(scaledCritData.scaledCritRate);
            if (!isCritical) { return (false, damages); }
            
            damages.ForEach(x => x.Value *= scaledCritData.scaledCritMultiplier);
            return (true, damages);
        }
        
        public virtual Attack GenerateAttack(EntityStatsVariables stats)
        {
            var damages = stats?.GetDamageReferences()
                .Where(x => x.StatValue != 0)
                .Select(x => ApplyVariance(new Damage(x.StatType, x.StatValue)))
                .ToArray();

            var outcome = AttemptToCritical(damages, stats);
            return new Attack(outcome.Damages, outcome.DidCrit);
        }
        
        public virtual Attack GenerateAttack(Ability ability, EntityStatsVariables stats)
        {
            var baseDamage = ability.Template.Variables.Damage();
            return GenerateAttack(baseDamage, stats);
        }
        
        public virtual Attack GenerateAttack(Damage damage, EntityStatsVariables stats)
        {
            damage.Value += stats.GetDamageFromDamageType(damage.Type);
            ApplyVariance(damage);
            var outcome = AttemptToCritical(new[] { damage }, stats);
            return new Attack(outcome.Damages, outcome.DidCrit);
        }
    }
}