using System.Collections.Generic;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Extensions;
using OpenRpg.Core.Variables;
using OpenRpg.Entities.Effects.Processors;
using OpenRpg.Entities.Entity.Populators.Stats;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Fantasy.Effects;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Stats.Populators.Partial
{
    public class FantasyElementalStatPopulator : IEntityPartialStatPopulator
    {
        public int Priority => 10;
        
        public float CalculateStatsWithModifier(ComputedEffects computedEffects, int effectAllBonusAmountType, int effectAllBonusPercentageType, EffectRelationship effectRelationship, float modifierValue = 0)
        {
            var baseTotal = computedEffects.CalculateTotalValueFor(effectRelationship, effectAllBonusAmountType, effectAllBonusPercentageType);
            var modifierBonus = baseTotal * modifierValue;
            return baseTotal + modifierBonus;
        }

        public void Populate(EntityStatsVariables stats, ComputedEffects computedEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            var intModifier = stats.Intelligence() / 100.0f;
            stats.IceDamage(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllElementDamageBonusAmount, FantasyEffectTypes.AllElementDamageBonusPercentage, EffectRelationships.IceDamageRelationship, intModifier));
            stats.FireDamage(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllElementDamageBonusAmount, FantasyEffectTypes.AllElementDamageBonusPercentage, EffectRelationships.FireDamageRelationship, intModifier));
            stats.WindDamage(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllElementDamageBonusAmount, FantasyEffectTypes.AllElementDamageBonusPercentage, EffectRelationships.WindDamageRelationship, intModifier));
            stats.EarthDamage(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllElementDamageBonusAmount, FantasyEffectTypes.AllElementDamageBonusPercentage, EffectRelationships.EarthDamageRelationship, intModifier));
            stats.LightDamage(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllElementDamageBonusAmount, FantasyEffectTypes.AllElementDamageBonusPercentage, EffectRelationships.LightDamageRelationship, intModifier));
            stats.DarkDamage(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllElementDamageBonusAmount, FantasyEffectTypes.AllElementDamageBonusPercentage, EffectRelationships.DarkDamageRelationship, intModifier));
            
            stats.IceDefense(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllElementDefenseBonusAmount, FantasyEffectTypes.AllElementDefenseBonusPercentage, EffectRelationships.IceDefenseRelationship, intModifier));
            stats.FireDefense(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllElementDefenseBonusAmount, FantasyEffectTypes.AllElementDefenseBonusPercentage, EffectRelationships.FireDefenseRelationship, intModifier));
            stats.WindDefense(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllElementDefenseBonusAmount, FantasyEffectTypes.AllElementDefenseBonusPercentage, EffectRelationships.WindDefenseRelationship, intModifier));
            stats.EarthDefense(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllElementDefenseBonusAmount, FantasyEffectTypes.AllElementDefenseBonusPercentage, EffectRelationships.EarthDefenseRelationship, intModifier));
            stats.LightDefense(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllElementDefenseBonusAmount, FantasyEffectTypes.AllElementDefenseBonusPercentage, EffectRelationships.LightDefenseRelationship, intModifier));
            stats.DarkDefense(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllElementDefenseBonusAmount, FantasyEffectTypes.AllElementDefenseBonusPercentage, EffectRelationships.DarkDefenseRelationship, intModifier));
        }
    }
}
