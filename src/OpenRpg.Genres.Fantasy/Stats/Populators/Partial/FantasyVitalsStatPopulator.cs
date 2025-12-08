using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Entities.Effects.Processors;
using OpenRpg.Entities.Entity.Populators.Stats;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Stats.Populators.Partial
{
    public class FantasyVitalsStatPopulator : IEntityPartialStatPopulator
    {
        public int Priority => 10;

        public void Populate(EntityStatsVariables stats, ComputedEffects computedEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            var constitutionBonus = stats.Constitution * 5;
            stats.MaxHealth = (int)computedEffects.CalculateTotalValueFor(FantasyEffectTypes.HealthBonusAmount, FantasyEffectTypes.HealthBonusPercentage, constitutionBonus);
            stats.MaxStamina  = (int)computedEffects.CalculateTotalValueFor(FantasyEffectTypes.StaminaBonusAmount, FantasyEffectTypes.StaminaBonusPercentage, constitutionBonus);
            
            var intelligenceBonus = stats.Intelligence * 5;
            stats.MaxMana = (int)computedEffects.CalculateTotalValueFor(FantasyEffectTypes.ManaBonusAmount, FantasyEffectTypes.ManaBonusPercentage, intelligenceBonus);
            
            stats.HealthRegen = computedEffects.CalculateTotalValueFor(FantasyEffectTypes.HealthRegenBonusAmount, FantasyEffectTypes.HealthRegenBonusPercentage);
            stats.ManaRegen = computedEffects.CalculateTotalValueFor(FantasyEffectTypes.ManaRegenBonusAmount, FantasyEffectTypes.ManaRegenBonusPercentage);
            stats.StaminaRegen = computedEffects.CalculateTotalValueFor(FantasyEffectTypes.StaminaRegenBonusAmount, FantasyEffectTypes.StaminaRegenBonusPercentage);
            
            stats.HealthRegenRate = computedEffects.CalculateTotalValueFor(FantasyEffectTypes.HealthRegenRateBonusAmount, FantasyEffectTypes.HealthRegenRateBonusPercentage);
            stats.ManaRegenRate = computedEffects.CalculateTotalValueFor(FantasyEffectTypes.ManaRegenRateBonusAmount, FantasyEffectTypes.ManaRegenRateBonusPercentage);
            stats.StaminaRegenRate = computedEffects.CalculateTotalValueFor(FantasyEffectTypes.StaminaRegenRateBonusAmount, FantasyEffectTypes.StaminaRegenRateBonusPercentage);
            
            stats.MovementSpeed = computedEffects.CalculateTotalValueFor(FantasyEffectTypes.MovementSpeedBonusAmount, FantasyEffectTypes.MovementSpeedBonusPercentage);
        }
    }
}