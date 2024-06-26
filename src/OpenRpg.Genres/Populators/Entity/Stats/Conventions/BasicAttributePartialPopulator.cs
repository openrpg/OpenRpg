using System;
using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Stats;
using OpenRpg.Core.Stats.Entity;
using OpenRpg.Core.Variables;
using OpenRpg.Genres.Extensions;

namespace OpenRpg.Genres.Populators.Entity.Stats.Conventions
{
    public class BasicAttributePartialPopulator : IEntityPartialStatPopulator
    {
        public int Priority { get; }
        public int EffectBonusAmountType { get; }
        public int EffectBonusPercentageType { get; }
        public int StatType { get; }
        public Func<IEntityStatsVariables, IReadOnlyCollection<Effect>, IReadOnlyCollection<IVariables>, int> MiscGetter { get; } = null;

        public BasicAttributePartialPopulator(int effectBonusAmountType, int effectBonusPercentageType, int statType, int priority = 100)
        {
            Priority = priority;
            EffectBonusAmountType = effectBonusAmountType;
            EffectBonusPercentageType = effectBonusPercentageType;
            StatType = statType;
        }
        
        public BasicAttributePartialPopulator(int effectBonusAmountType, int effectBonusPercentageType, int statType,
            Func<IEntityStatsVariables, IReadOnlyCollection<Effect>, IReadOnlyCollection<IVariables>, int> miscGetter,  
            int priority = 100)
            : this(effectBonusAmountType, effectBonusPercentageType, statType)
        {
            MiscGetter = miscGetter;
        }

        public void Populate(IEntityStatsVariables stats, IReadOnlyCollection<Effect> activeEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            var miscBonus = MiscGetter?.Invoke(stats, activeEffects, relatedVars) ?? 0;
            var attributeValue = (int)activeEffects.CalculateAttributeValueFor(EffectBonusAmountType, EffectBonusPercentageType, miscBonus);
            stats[StatType] = attributeValue;
        }
    }
}