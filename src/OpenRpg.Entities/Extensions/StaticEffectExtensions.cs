using System.Collections.Generic;
using System.Linq;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Effects.Processors;

namespace OpenRpg.Entities.Extensions
{
    public static class StaticEffectExtensions
    {

        
        // TODO: This is a crutch until the IEffect stuff is fully mapped over
        public static IReadOnlyCollection<StaticEffect> GetStaticEffects(this IReadOnlyCollection<IEffect> effects) => 
            effects.Where(x => x is StaticEffect).Cast<StaticEffect>().ToArray();
    }
}