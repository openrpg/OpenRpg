using System.Collections.Generic;
using OpenRpg.Core.Stats;

namespace OpenRpg.Core.Defaults
{
    public class DefaultEntityStats : IEntityStats
    {
        public IDictionary<int, float> Stats { get; } = new Dictionary<int, float>();
    }
}