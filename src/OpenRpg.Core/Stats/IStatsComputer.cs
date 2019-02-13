using System.Collections.Generic;
using OpenRpg.Core.Effects;

namespace OpenRpg.Core.Stats
{
    public interface IStatsComputer
    {
        IEntityStats ComputeStats(ICustomStatData customStatData, IReadOnlyCollection<Effect> effects);
    }
}