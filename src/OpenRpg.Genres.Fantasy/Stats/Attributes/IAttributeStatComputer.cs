using System.Collections.Generic;
using OpenRpg.Core.Effects;

namespace OpenRpg.Genres.Fantasy.Stats.Attributes
{
    public interface IAttributeStatComputer
    {
        IAttributeStats ComputeStats(IAttributeStats baseAttributeStats, ICollection<Effect> activeEffects);
    }
}