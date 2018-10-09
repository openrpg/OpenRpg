using System.Collections.Generic;

namespace OpenRpg.Effects
{
    public interface IHasEffects
    {
        IEnumerable<Effect> Effects { get; }
    }
}