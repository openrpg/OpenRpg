using System.Collections.Generic;
using OpenRpg.Core.Effects;

namespace OpenRpg.Core.Stats
{
    public interface IStatPopulator
    {
        void PopulateStats(IStatsVariables stats, IReadOnlyCollection<Effect> activeEffects);
    }
}