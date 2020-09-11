using System.Collections.Generic;
using OpenRpg.Core.Effects;

namespace OpenRpg.Core.Stats
{
    public interface IStatPopulator
    {
        void PopulateStats(IStatsVariables statses, IReadOnlyCollection<Effect> activeEffects);
    }
}