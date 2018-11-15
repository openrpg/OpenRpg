using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Effects;

namespace OpenRpg.Core.Extensions
{
    public static class EffectExtensions
    {
        public static int GetPotencyFor(this IEnumerable<Effect> effects, int effectType)
        { return (int)effects.Where(x => x.EffectType == effectType).Sum(x => x.Potency); }
        
        public static int GetPotencyFor(this IEnumerable<Effect> effects, params int[] effectTypes)
        { return (int)effects.Where(x => effectTypes.Contains(x.EffectType)).Sum(x => x.Potency); }
    }
}