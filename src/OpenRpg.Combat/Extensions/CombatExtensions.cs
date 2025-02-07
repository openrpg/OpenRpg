using System.Collections.Generic;
using OpenRpg.Combat.Attacks;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Extensions;

namespace OpenRpg.Combat.Extensions
{
    public static class CombatExtensions
    {
        public static float CalculateTotalAmount(this IEnumerable<StaticEffect> effects, int effectAmountType, int bonusAmountType)
        { return effects.GetPotencyFor(effectAmountType, bonusAmountType); }
        
        public static float CalculateTotalPercentage(this IEnumerable<StaticEffect> effects, int effectPercentageType, int bonusPercentageType)
        { return effects.GetPotencyFor(effectPercentageType, bonusPercentageType); }
        
        public static float CalculateTotal(this IEnumerable<StaticEffect> effects, int effectAmountType, int effectPercentageType, int bonusAmountType, int bonusPercentageType)
        {
            var baseValue = effects.CalculateTotalAmount(effectAmountType, bonusAmountType);
            if(baseValue == 0) { return 0; }
            
            var percentageBonus = effects.CalculateTotalPercentage(effectPercentageType, bonusPercentageType);
            if(percentageBonus != 0) { percentageBonus /= 100;}

            var percentageValue = baseValue * percentageBonus;
            return baseValue + percentageValue;
        }
        
        public static float CalculateTotal(this IEnumerable<StaticEffect> effects, EffectRelationship damageRelationship)
        {
            return effects.CalculateTotal(damageRelationship.AmountType, damageRelationship.PercentageType,
                damageRelationship.BonusAmountType, damageRelationship.BonusPercentageType);
        }
    }
}