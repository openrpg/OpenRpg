using System.Collections.Generic;

namespace OpenRpg.Core.Effects
{
    public interface IHasEffects
    {
        IReadOnlyCollection<IEffect> Effects { get; }
    }
}