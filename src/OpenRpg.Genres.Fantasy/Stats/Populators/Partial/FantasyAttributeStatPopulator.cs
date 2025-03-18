using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Entities.Effects.Processors;
using OpenRpg.Entities.Entity.Populators.Stats;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Stats.Populators.Partial
{
    public class FantasyAttributeStatPopulator : IEntityPartialStatPopulator
    {
        public int Priority => 100;

        public int CalculateStateFor(ComputedEffects computedEffects, int amountType, int percentageType)
        {
            return (int)computedEffects.CalculateTotalValueFor(
                new[] { FantasyEffectTypes.AllAttributeBonusAmount, amountType },
                new[] { FantasyEffectTypes.AllAttributeBonusPercentage, percentageType });
        }
        
        public void Populate(EntityStatsVariables stats, ComputedEffects computedEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            stats.Strength(CalculateStateFor(computedEffects, FantasyEffectTypes.StrengthBonusAmount, FantasyEffectTypes.StrengthBonusPercentage));
            stats.Dexterity(CalculateStateFor(computedEffects, FantasyEffectTypes.DexterityBonusAmount, FantasyEffectTypes.DexterityBonusPercentage));
            stats.Constitution(CalculateStateFor(computedEffects, FantasyEffectTypes.ConstitutionBonusAmount, FantasyEffectTypes.ConstitutionBonusPercentage));
            stats.Intelligence(CalculateStateFor(computedEffects, FantasyEffectTypes.IntelligenceBonusAmount, FantasyEffectTypes.IntelligenceBonusPercentage));
            stats.Wisdom(CalculateStateFor(computedEffects, FantasyEffectTypes.WisdomBonusAmount, FantasyEffectTypes.WisdomBonusPercentage));
            stats.Charisma(CalculateStateFor(computedEffects, FantasyEffectTypes.CharismaBonusAmount, FantasyEffectTypes.CharismaBonusPercentage));
        }
    }
}