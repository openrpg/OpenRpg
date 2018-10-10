using System.Collections.Generic;

namespace OpenRpg.Core.Effects
{
    public interface IHasEffects
    {
        IEnumerable<Effect> Effects { get; }
    }
}