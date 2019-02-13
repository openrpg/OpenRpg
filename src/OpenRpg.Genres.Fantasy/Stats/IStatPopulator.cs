using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Stats;

namespace OpenRpg.Genres.Fantasy.Stats
{
    public interface IStatPopulator
    {
        void PopulateStats(IEntityStats stats, ICustomStatData customStatData, IReadOnlyCollection<Effect> activeEffects);
    }
}