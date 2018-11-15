using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Stats.Attributes;

namespace OpenRpg.Genres.Fantasy.Stats.Damage
{
    public interface IDamageStatComputer
    {
        IDamageStats ComputeStats(IAttributeStats baseAttributeStats, ICollection<Effect> effects);
    }
}