using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class EffectExtensions
    {
        public static int CalculateBonusFor(this IReadOnlyCollection<Effect> activeEffects, int amountBonusType, int percentageBonusType, int miscBonus = 0)
        {
            var totalAmount = activeEffects.GetPotencyFor(amountBonusType) + miscBonus;
            var percentageBonus = activeEffects.GetPotencyFor(percentageBonusType);
            var totalBonus = totalAmount * percentageBonus;
            return totalAmount + totalBonus;
        }
    }
}