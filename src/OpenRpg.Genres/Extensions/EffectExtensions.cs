using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Genres.Types;

namespace OpenRpg.Genres.Extensions
{
    public static class EffectExtensions
    {
        /// <summary>
        /// This will return a total value based on the underlying amount type factoring in
        /// the percentage and a misc bonus if provided
        /// </summary>
        /// <param name="effects">The effects that are applied</param>
        /// <param name="amountBonusType">The effect type signifying the bonus amount</param>
        /// <param name="percentageBonusType">The effect type signifying the percentage amount</param>
        /// <param name="miscBonus">Any misc bonus to apply to the calculation (calculated before percentage applied)</param>
        /// <returns>The total value based off effects of the given types</returns>
        public static float CalculateStatValueFor(this IReadOnlyCollection<Effect> effects, int amountBonusType, int percentageBonusType, int miscBonus = 0)
        {
            var totalAmount = effects.GetPotencyFor(amountBonusType) + miscBonus;
            if(totalAmount == 0) { return 0; }
            
            var percentageBonus = effects.GetPotencyFor(percentageBonusType);
            var totalBonus = totalAmount * percentageBonus;
            return totalAmount + totalBonus;
        }
        
        /// <summary>
        /// Calculates the attribute value based off the underlying attribute amount with all attributes factored in
        /// </summary>
        /// <param name="activeEffects"></param>
        /// <param name="attributeAmountType"></param>
        /// <param name="attributePercentageType"></param>
        /// <param name="miscBonus"></param>
        /// <returns></returns>
        public static float CalculateAttributeValueFor(this IReadOnlyCollection<Effect> activeEffects, int attributeAmountType, int attributePercentageType, int miscBonus = 0)
        {
            var totalAmount = activeEffects.GetPotencyFor(attributeAmountType, GenreEffectTypes.AllAttributeBonusAmount) + miscBonus;
            if(totalAmount == 0) { return 0; }
            
            var percentageBonus = activeEffects.GetPotencyFor(attributePercentageType, GenreEffectTypes.AllAttributeBonusPercentage);
            var totalBonus = totalAmount * percentageBonus;
            return totalAmount + totalBonus;
        }
    }
}