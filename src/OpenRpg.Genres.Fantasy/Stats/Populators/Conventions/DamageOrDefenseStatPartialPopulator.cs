using System;
using System.Collections.Generic;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Extensions;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Stats.Entity;
using OpenRpg.Core.Variables;
using OpenRpg.Genres.Populators.Entity.Stats;

namespace OpenRpg.Genres.Fantasy.Stats.Populators.Conventions
{
    public class DamageOrDefenseStatPartialPopulator : IEntityPartialStatPopulator
    {
        public int Priority { get; }
        public int EffectAllBonusAmountType { get; }
        public int EffectAllBonusPercentageType { get; }
        public int StatType { get; }
        public EffectRelationship EffectRelationship { get; }
        public Func<IEntityStatsVariables, IReadOnlyCollection<Effect>, IReadOnlyCollection<IVariables>, float> ModBonusGetter { get; }
        
        public DamageOrDefenseStatPartialPopulator(int effectAllBonusAmountType, int effectAllBonusPercentageType, int statType, EffectRelationship effectRelationship,
            Func<IEntityStatsVariables, IReadOnlyCollection<Effect>, IReadOnlyCollection<IVariables>, float> modBonusGetter, int priority = 100)
        {
            Priority = priority;
            EffectAllBonusAmountType = effectAllBonusAmountType;
            EffectAllBonusPercentageType = effectAllBonusPercentageType;
            StatType = statType;
            EffectRelationship = effectRelationship;
            ModBonusGetter = modBonusGetter;
        }
        
        public void Populate(IEntityStatsVariables stats, IReadOnlyCollection<Effect> activeEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            var baseTotal = activeEffects.CalculateTotal(EffectRelationship);
            baseTotal += activeEffects.GetPotencyFor(EffectAllBonusAmountType);

            if (baseTotal == 0)
            {
                stats[StatType] = 0;
                return;
            }

            var percentageBonus = activeEffects.GetPotencyFor(EffectAllBonusPercentageType);
            if (percentageBonus != 0)
            { 
                var addition = baseTotal * (percentageBonus/100);
                baseTotal += addition;
            }
            
            var modifierBonus = baseTotal * ModBonusGetter(stats, activeEffects, relatedVars);
            var finalTotal = baseTotal + modifierBonus;

            stats[StatType] = finalTotal;
        }
    }
}