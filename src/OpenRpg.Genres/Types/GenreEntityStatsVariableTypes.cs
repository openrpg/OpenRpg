using OpenRpg.Core.Types;

namespace OpenRpg.Genres.Types
{
    public interface GenreEntityStatsVariableTypes : EntityStatsVariableTypes
    {
        // Base stats
        public static readonly int MaxHealth = 1;
        
        // Attack stats
        public static readonly int Damage = 10;
        
        // Defense stats
        public static readonly int Defense = 20;
    }
}