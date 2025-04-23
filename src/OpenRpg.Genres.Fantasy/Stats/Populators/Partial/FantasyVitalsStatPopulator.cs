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
            var constitutionBonus = stats.Constitution() * 5;
            var maxHealth = (int)computedEffects.CalculateTotalValueFor(FantasyEffectTypes.HealthBonusAmount, FantasyEffectTypes.HealthBonusPercentage, constitutionBonus);
            stats.MaxHealth(maxHealth);
            
            var maxStamina = (int)computedEffects.CalculateTotalValueFor(FantasyEffectTypes.StaminaBonusAmount, FantasyEffectTypes.StaminaBonusPercentage, constitutionBonus);
            stats.MaxStamina(maxStamina);
            
            var intelligenceBonus = stats.Intelligence() * 5;
            var maxMana = (int)computedEffects.CalculateTotalValueFor(FantasyEffectTypes.ManaBonusAmount, FantasyEffectTypes.ManaBonusPercentage, intelligenceBonus);
            stats.MaxMana(maxMana);
            
            stats.HealthRegen(computedEffects.CalculateTotalValueFor(FantasyEffectTypes.HealthRegenBonusAmount, FantasyEffectTypes.HealthRegenBonusPercentage));
            stats.ManaRegen(computedEffects.CalculateTotalValueFor(FantasyEffectTypes.ManaRegenBonusAmount, FantasyEffectTypes.ManaRegenBonusPercentage));
            stats.StaminaRegen(computedEffects.CalculateTotalValueFor(FantasyEffectTypes.StaminaRegenBonusAmount, FantasyEffectTypes.StaminaRegenBonusPercentage));
        }
    }
}