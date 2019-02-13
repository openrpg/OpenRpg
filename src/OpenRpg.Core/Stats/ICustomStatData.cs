using System.Collections.Generic;

namespace OpenRpg.Core.Stats
{
    public interface ICustomStatData
    {
        IDictionary<int, float> CustomStatData { get; }
    }
}