using System.Collections.Generic;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Extensions;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Stats;
using OpenRpg.Genres.Fantasy.Characters;
using OpenRpg.Genres.Fantasy.Effects;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Stats;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Defaults
{
    public class DefaultDefenseStatPopulator : IDefenseStatPopulator
    {
        public float ComputeDefense(float statModifier, EffectRelationship damageRelationship, IReadOnlyCollection<Effect> effects)
        {
            var totalDamage = effects.CalculateTotal(damageRelationship);
            if(totalDamage == 0) { return 0; }

            var modifierBonus = totalDamage * statModifier;
            return totalDamage + modifierBonus;
        }
        
        public float ComputeIceDefense(IEntityStats baseAttributeStats, IReadOnlyCollection<Effect> effects)
        { return ComputeDefense(baseAttributeStats.Intelligence() / 100, EffectRelationships.IceDefenseRelationship, effects); }
        
        public float ComputeFireDefense(IEntityStats baseAttributeStats, IReadOnlyCollection<Effect> effects)
        { return ComputeDefense(baseAttributeStats.Intelligence() / 100, EffectRelationships.FireDefenseRelationship, effects); }
        
        public float ComputeWindDefense(IEntityStats baseAttributeStats, IReadOnlyCollection<Effect> effects)
        { return ComputeDefense(baseAttributeStats.Intelligence() / 100, EffectRelationships.WindDefenseRelationship, effects); }
        
        public float ComputeEarthDefense(IEntityStats baseAttributeStats, IReadOnlyCollection<Effect> effects)
        { return ComputeDefense(baseAttributeStats.Intelligence() / 100, EffectRelationships.EarthDefenseRelationship, effects); }
        
        public float ComputeLightDefense(IEntityStats baseAttributeStats, IReadOnlyCollection<Effect> effects)
        { return ComputeDefense(baseAttributeStats.Intelligence() / 100, EffectRelationships.LightDefenseRelationship, effects); }
        
        public float ComputeDarkDefense(IEntityStats baseAttributeStats, IReadOnlyCollection<Effect> effects)
        { return ComputeDefense(baseAttributeStats.Intelligence() / 100, EffectRelationships.DarkDefenseRelationship, effects); }

        public float ComputeSlashingDefense(IEntityStats baseAttributeStats, IReadOnlyCollection<Effect> effects)
        {
            var strengthBonus = baseAttributeStats.Strength() / 200;
            var dexterityBonus = baseAttributeStats.Dexterity() / 200;
            return ComputeDefense(strengthBonus + dexterityBonus, EffectRelationships.SlashingDefenseRelationship, effects);
        }
        
        public float ComputeBluntDefense(IEntityStats baseAttributeStats, IReadOnlyCollection<Effect> effects)
        { return ComputeDefense(baseAttributeStats.Strength() / 100, EffectRelationships.BluntDefenseRelationship, effects); }
        
        public float ComputePiercingDefense(IEntityStats baseAttributeStats, IReadOnlyCollection<Effect> effects)
        { return ComputeDefense(baseAttributeStats.Dexterity() / 100, EffectRelationships.PiercingDefenseRelationship, effects); }
        
        public float ComputeUnarmedDefense(IEntityStats baseAttributeStats, IReadOnlyCollection<Effect> effects)
        {
            var strengthBonus = baseAttributeStats.Strength() / 200;
            var dexterityBonus = baseAttributeStats.Dexterity() / 200;
            return ComputeDefense(strengthBonus + dexterityBonus, EffectRelationships.UnarmedDefenseRelationship, effects);
        }       

        public void PopulateStats(IEntityStats stats, ICustomStatData customStatData, IReadOnlyCollection<Effect> activeEffects)
        {
            var iceDefense = ComputeIceDefense(stats, activeEffects);
            var fireDefense = ComputeFireDefense(stats, activeEffects);
            var windDefense = ComputeWindDefense(stats, activeEffects);
            var earthDefense = ComputeEarthDefense(stats, activeEffects);
            var lightDefense = ComputeLightDefense(stats, activeEffects);
            var darkDefense = ComputeDarkDefense(stats, activeEffects);
            var slashingDefense = ComputeSlashingDefense(stats, activeEffects);
            var bluntDefense = ComputeBluntDefense(stats, activeEffects);
            var piercingDefense = ComputePiercingDefense(stats, activeEffects);
            var unarmedDefense = ComputeUnarmedDefense(stats, activeEffects);

            stats.IceDefense(iceDefense);
            stats.FireDefense(fireDefense);
            stats.WindDefense(windDefense);
            stats.EarthDefense(earthDefense);
            stats.LightDefense(lightDefense);
            stats.DarkDefense(darkDefense);
            stats.SlashingDefense(slashingDefense);
            stats.BluntDefense(bluntDefense);
            stats.PiercingDefense(piercingDefense);
            stats.UnarmedDefense(unarmedDefense);
        }
    }
}