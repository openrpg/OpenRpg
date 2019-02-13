using System.Collections.Generic;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Extensions;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Stats;
using OpenRpg.Genres.Fantasy.Effects;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Stats;

namespace OpenRpg.Genres.Fantasy.Defaults
{
    public class DefaultDamageStatPopulator : IDamageStatPopulator
    {
        public float ComputeDamage(float statModifier, EffectRelationship damageRelationship, IReadOnlyCollection<Effect> effects)
        {
            var totalDamage = effects.CalculateTotal(damageRelationship);
            if(totalDamage == 0) { return 0; }

            var modifierBonus = totalDamage * statModifier;
            return totalDamage + modifierBonus;
        }
        
        public float ComputeIceDamage(IEntityStats stats, IReadOnlyCollection<Effect> effects)
        { return ComputeDamage(stats.Intelligence() / 100, EffectRelationships.IceDamageRelationship, effects); }
        
        public float ComputeFireDamage(IEntityStats stats, IReadOnlyCollection<Effect> effects)
        { return ComputeDamage(stats.Intelligence() / 100, EffectRelationships.FireDamageRelationship, effects); }
        
        public float ComputeWindDamage(IEntityStats stats, IReadOnlyCollection<Effect> effects)
        { return ComputeDamage(stats.Intelligence() / 100, EffectRelationships.WindDamageRelationship, effects); }
        
        public float ComputeEarthDamage(IEntityStats stats, IReadOnlyCollection<Effect> effects)
        { return ComputeDamage(stats.Intelligence() / 100, EffectRelationships.EarthDamageRelationship, effects); }
        
        public float ComputeLightDamage(IEntityStats stats, IReadOnlyCollection<Effect> effects)
        { return ComputeDamage(stats.Intelligence() / 100, EffectRelationships.LightDamageRelationship, effects); }
        
        public float ComputeDarkDamage(IEntityStats stats, IReadOnlyCollection<Effect> effects)
        { return ComputeDamage(stats.Intelligence() / 100, EffectRelationships.DarkDamageRelationship, effects); }

        public float ComputeSlashingDamage(IEntityStats stats, IReadOnlyCollection<Effect> effects)
        {
            var strengthBonus = stats.Strength() / 200;
            var dexterityBonus = stats.Dexterity() / 200;
            return ComputeDamage(strengthBonus + dexterityBonus, EffectRelationships.SlashingDamageRelationship, effects);
        }
        
        public float ComputeBluntDamage(IEntityStats stats, IReadOnlyCollection<Effect> effects)
        { return ComputeDamage(stats.Strength() / 100, EffectRelationships.BluntDamageRelationship, effects); }
        
        public float ComputePiercingDamage(IEntityStats stats, IReadOnlyCollection<Effect> effects)
        { return ComputeDamage(stats.Dexterity() / 100, EffectRelationships.PiercingDamageRelationship, effects); }
        
        public float ComputeUnarmedDamage(IEntityStats stats, IReadOnlyCollection<Effect> effects)
        {
            var strengthBonus = stats.Strength() / 200;
            var dexterityBonus = stats.Dexterity() / 200;
            return ComputeDamage(strengthBonus + dexterityBonus, EffectRelationships.UnarmedDamageRelationship, effects);
        }
        
        public void PopulateStats(IEntityStats stats, ICustomStatData customStatData, IReadOnlyCollection<Effect> activeEffects)
        {
            var iceDamage = ComputeIceDamage(stats, activeEffects);
            var fireDamage = ComputeFireDamage(stats, activeEffects);
            var windDamage = ComputeWindDamage(stats, activeEffects);
            var earthDamage = ComputeEarthDamage(stats, activeEffects);
            var lightDamage = ComputeLightDamage(stats, activeEffects);
            var darkDamage = ComputeDarkDamage(stats, activeEffects);
            var slashingDamage = ComputeSlashingDamage(stats, activeEffects);
            var bluntDamage = ComputeBluntDamage(stats, activeEffects);
            var piercingDamage = ComputePiercingDamage(stats, activeEffects);
            var unarmedDamage = ComputeUnarmedDamage(stats, activeEffects);

            stats.IceDamage(iceDamage);
            stats.FireDamage(fireDamage);
            stats.WindDamage(windDamage);
            stats.EarthDamage(earthDamage);
            stats.LightDamage(lightDamage);
            stats.DarkDamage(darkDamage);
            stats.SlashingDamage(slashingDamage);
            stats.BluntDamage(bluntDamage);
            stats.PiercingDamage(piercingDamage);
            stats.UnarmedDamage(unarmedDamage);
        }
    }
}