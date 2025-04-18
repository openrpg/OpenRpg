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
            
            var intelligenceBonus = stats.Intelligence() * 5;
            var maxMagic = (int)computedEffects.CalculateTotalValueFor(FantasyEffectTypes.MagicBonusAmount, FantasyEffectTypes.MagicBonusPercentage, intelligenceBonus);
            stats.MaxMagic(maxMagic);
            
            stats.HealthRegen(computedEffects.CalculateTotalValueFor(FantasyEffectTypes.HealthRegenBonusAmount, FantasyEffectTypes.HealthRegenBonusPercentage));
            stats.MagicRegen(computedEffects.CalculateTotalValueFor(FantasyEffectTypes.MagicRegenBonusAmount, FantasyEffectTypes.MagicRegenBonusPercentage));
        }
    }
}