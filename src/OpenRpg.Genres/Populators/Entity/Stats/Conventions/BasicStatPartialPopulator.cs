using System;
using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Effects.Processors;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Extensions;

namespace OpenRpg.Genres.Populators.Entity.Stats.Conventions
{
    public class BasicStatPartialPopulator : IEntityPartialStatPopulator
    {
        public int Priority { get; }
        public int EffectBonusAmountType { get; }
        public int EffectBonusPercentageType { get; }
        public int StatType { get; }

        public BasicStatPartialPopulator(int effectBonusAmountType, int effectBonusPercentageType, int statType, int priority = 100)
        {
            Priority = priority;
            EffectBonusAmountType = effectBonusAmountType;
            EffectBonusPercentageType = effectBonusPercentageType;
            StatType = statType;
        }

        public void Populate(EntityStatsVariables stats, ComputedEffects computedEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            var attributeValue = (int)computedEffects.CalculateTotalValueFor(EffectBonusAmountType, EffectBonusPercentageType);
            stats[StatType] = attributeValue;
        }
    }
}