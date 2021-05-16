using System.Collections.Generic;
using OpenRpg.Core.Effects;

namespace OpenRpg.Core.Stats
{
    public interface IStatsComputer
    {
        IStatsVariables ComputeStats(IReadOnlyCollection<Effect> effects);
        void RecomputeStats(IStatsVariables stats, IReadOnlyCollection<Effect> activeEffects);
    }
}