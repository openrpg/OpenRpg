using System.Collections.Generic;
using System.Linq;
using OpenRpg.Entities.Effects;

namespace OpenRpg.Entities.Extensions
{
    public static class StaticEffectExtensions
    {
        public static float GetPotencyFor(this IEnumerable<StaticEffect> effects, int effectType)
        { return effects.Where(x => x.EffectType == effectType).Sum(x => x.Potency); }
        
        public static float GetPotencyFor(this IEnumerable<StaticEffect> effects, params int[] effectTypes)
        { return effects.Where(x => effectTypes.Contains(x.EffectType)).Sum(x => x.Potency); }
    }
}