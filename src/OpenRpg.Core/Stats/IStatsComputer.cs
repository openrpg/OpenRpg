using System.Collections.Generic;
using OpenRpg.Core.Effects;

namespace OpenRpg.Core.Stats
{
    public interface IStatsComputer
    {
        IEntityStats ComputeStats(IReadOnlyCollection<Effect> effects);
    }
}