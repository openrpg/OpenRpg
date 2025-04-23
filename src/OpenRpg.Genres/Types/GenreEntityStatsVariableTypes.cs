using OpenRpg.Entities.Types;

namespace OpenRpg.Genres.Types
{
    public interface GenreEntityStatsVariableTypes : CoreEntityStatsVariableTypes
    {
        // Base stats
        public static readonly int MaxHealth = 1;
        public static readonly int HealthRegen = 2;
        public static readonly int MaxStamina = 3;
        public static readonly int StaminaRegen = 4;
        public static readonly int MovementSpeed = 5;
        public static readonly int HealthRegenRate = 6;
        public static readonly int StaminaRegenRate = 7;
        
        // Attack stats
        public static readonly int Damage = 10;
        public static readonly int CriticalDamageChance = 11;
        public static readonly int CriticalDamageMultiplier = 12;
        
        // Defense stats
        public static readonly int Defense = 20;
        
        // Ability stats
        static readonly int AttackRange = 30;
        static readonly int AttackSize = 31;
        static readonly int CooldownReduction = 32;
    }
}