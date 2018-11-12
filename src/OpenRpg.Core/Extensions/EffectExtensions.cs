using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Effects;

namespace OpenRpg.Core.Extensions
{
    public static class EffectExtensions
    {
        public static byte GetPotencyFor(this IEnumerable<Effect> effects, byte effectType)
        { return (byte)effects.Where(x => x.EffectType == effectType).Sum(x => x.Potency); }

        public static float GetTotalFor(this ICollection<Effect> effects, byte valueType, byte bonusType)
        {
            var baseValue = effects.GetPotencyFor(valueType);
            if(baseValue == 0) { return 0; }

            var bonus = effects.GetPotencyFor(bonusType);
            if(bonus == 0){ bonus = 1; }
            
            return baseValue * bonus;
        }
    }
}