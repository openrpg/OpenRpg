using System.Collections.Generic;

namespace OpenRpg.Combat.Effects
{
    public interface IHasActiveEffects
    {
        IList<ActiveEffect> ActiveEffects { get; }
    }
}