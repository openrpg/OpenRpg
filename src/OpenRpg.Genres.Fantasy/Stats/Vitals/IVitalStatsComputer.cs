using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Stats.Attributes;

namespace OpenRpg.Genres.Fantasy.Stats.Vitals
{
    public interface IVitalStatsComputer
    {
        IVitalStats ComputeStats(IVitalStats baseVitalStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects);
    }
}