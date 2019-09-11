using System.Collections.Generic;
using OpenRpg.Core.Effects;

namespace OpenRpg.Core.Stats
{
    public interface IStatPopulator
    {
        void PopulateStats(IEntityStats stats, IReadOnlyCollection<Effect> activeEffects);
    }
}