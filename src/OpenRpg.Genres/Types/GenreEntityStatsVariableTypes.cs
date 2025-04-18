using OpenRpg.Entities.Types;

namespace OpenRpg.Genres.Types
{
    public interface GenreEntityStatsVariableTypes : CoreEntityStatsVariableTypes
    {
        // Base stats
        public static readonly int MaxHealth = 1;
        public static readonly int HealthRegen = 2;
        
        // Attack stats
        public static readonly int Damage = 10;
        
        // Defense stats
        public static readonly int Defense = 20;
        
        // Ability stats
        static readonly int AttackRange = 30;
        static readonly int AttackSize = 31;
        static readonly int CooldownReduction = 32;
    }
}