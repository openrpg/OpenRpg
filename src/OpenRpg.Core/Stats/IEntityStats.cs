using System.Collections.Generic;

namespace OpenRpg.Core.Stats
{
    public interface IEntityStats
    {
        IDictionary<int, float> Stats { get; }
    }
}