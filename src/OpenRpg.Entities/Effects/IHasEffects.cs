using System.Collections.Generic;

namespace OpenRpg.Entities.Effects
{
    public interface IHasEffects
    {
        IReadOnlyCollection<IEffect> Effects { get; }
    }
}