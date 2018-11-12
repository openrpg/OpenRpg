using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Stats.Attributes;

namespace OpenRpg.Genres.Fantasy.Stats
{
    public interface ICharacterStatsComputer
    {
        ICharacterStats ComputeStats(IAttributeStats alteredStats, ICollection<Effect> effects);
    }
}