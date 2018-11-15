using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Stats.Attributes;

namespace OpenRpg.Genres.Fantasy.Stats.Defense
{
    public interface IDefenseStatComputer
    {
        IDefenseStats ComputeStats(IAttributeStats baseAttributeStats, ICollection<Effect> effects);
    }
}