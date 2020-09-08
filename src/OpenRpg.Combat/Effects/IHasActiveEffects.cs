using System.Collections.Generic;

namespace OpenRpg.Combat.Effects
{
    public interface IHasActiveEffects
    {
        IEnumerable<ActiveEffect> ActiveEffects { get; }
    }
}