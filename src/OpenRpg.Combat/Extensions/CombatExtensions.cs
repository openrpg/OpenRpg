using OpenRpg.Combat.Attacks;
using OpenRpg.Entities.Effects.Processors;
using OpenRpg.Entities.Extensions;

namespace OpenRpg.Combat.Extensions
{
    public static class CombatExtensions
    {
        public static float CalculateTotalValueFor(this ComputedEffects computedEffects, EffectRelationship effectRelationship, float miscBonus = 0)
        {
            var total = computedEffects.CalculateTotalValueFor(
                new[]{ effectRelationship.AmountType, effectRelationship.BonusAmountType}, 
                new[]{ effectRelationship.PercentageType, effectRelationship.BonusPercentageType });
            
            return total + miscBonus;
        }
        
        public static float CalculateTotalValueFor(this ComputedEffects computedEffects, EffectRelationship effectRelationship, int otherBonusType, int otherPercentageType, float miscBonus = 0)
        {
            var baseTotal = computedEffects.CalculateTotalValueFor(effectRelationship, miscBonus);
            baseTotal += computedEffects.Get(otherBonusType);

            if (baseTotal == 0)
            { return 0; }

            var percentageBonus = computedEffects.Get(otherPercentageType);
            if (percentageBonus != 0)
            { 
                var addition = baseTotal * (percentageBonus/100);
                baseTotal += addition;
            }
            
            return baseTotal;
        }
    }
}