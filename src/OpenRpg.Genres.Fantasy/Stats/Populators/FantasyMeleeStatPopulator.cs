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
using OpenRpg.Genres.Populators.Entity.Stats;

namespace OpenRpg.Genres.Fantasy.Stats.Populators
{
    public class FantasyMeleeStatPopulator : IEntityPartialStatPopulator
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
            var bluntModifier = stats.Strength() / 100.0f; 
            var piercingModifier = stats.Dexterity() / 100.0f;
            var slashingOrUnarmedModifier = (bluntModifier + piercingModifier) / 2;
            
            stats.BluntDamage(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllMeleeAttackBonusAmount, FantasyEffectTypes.AllMeleeAttackBonusPercentage, EffectRelationships.BluntDamageRelationship, bluntModifier));
            stats.PiercingDamage(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllMeleeAttackBonusAmount, FantasyEffectTypes.AllMeleeAttackBonusPercentage, EffectRelationships.PiercingDamageRelationship, piercingModifier));
            stats.SlashingDamage(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllMeleeAttackBonusAmount, FantasyEffectTypes.AllMeleeAttackBonusPercentage, EffectRelationships.SlashingDamageRelationship, slashingOrUnarmedModifier));
            stats.UnarmedDamage(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllMeleeAttackBonusAmount, FantasyEffectTypes.AllMeleeAttackBonusPercentage, EffectRelationships.UnarmedDamageRelationship, slashingOrUnarmedModifier));
            
            stats.BluntDefense(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllMeleeDefenseBonusPercentage, FantasyEffectTypes.AllMeleeDefenseBonusPercentage, EffectRelationships.BluntDefenseRelationship, bluntModifier));
            stats.PiercingDefense(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllMeleeDefenseBonusPercentage, FantasyEffectTypes.AllMeleeDefenseBonusPercentage, EffectRelationships.PiercingDefenseRelationship, piercingModifier));
            stats.SlashingDefense(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllMeleeDefenseBonusPercentage, FantasyEffectTypes.AllMeleeDefenseBonusPercentage, EffectRelationships.SlashingDefenseRelationship, slashingOrUnarmedModifier));
            stats.UnarmedDefense(CalculateStatsWithModifier(computedEffects, FantasyEffectTypes.AllMeleeDefenseBonusPercentage, FantasyEffectTypes.AllMeleeDefenseBonusPercentage, EffectRelationships.UnarmedDefenseRelationship, slashingOrUnarmedModifier));

        }
    }
}