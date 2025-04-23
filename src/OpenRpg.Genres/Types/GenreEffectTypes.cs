namespace OpenRpg.Genres.Types
{
    public interface GenreEffectTypes
    {
        public static readonly int UnknownBonus = 0;
        
        // Pure Damage Types
        public static readonly int DamageBonusAmount = 1;
        public static readonly int DamageBonusPercentage = 2;
        
        // Damage Critical Types
        public static readonly int CriticalRateBonusAmount = 3;
        public static readonly int CriticalRateBonusPercentage = 4;
        static readonly int CriticalDamageBonusAmount = 5;
        static readonly int CriticalDamageBonusPercentage = 6;
        
        // Pure Defense Types
        public static readonly int DefenseBonusAmount = 10;
        public static readonly int DefenseBonusPercentage = 11;
        
        // Attributes
        public static readonly int AllAttributeBonusAmount = 20;
        public static readonly int AllAttributeBonusPercentage = 21;
        public static readonly int HealthBonusAmount = 22;
        public static readonly int HealthRestoreAmount = 23;
        public static readonly int HealthRegenBonusAmount = 24;
        public static readonly int HealthRegenBonusPercentage = 25;
        public static readonly int StaminaBonusAmount = 26;
        public static readonly int StaminaRestoreAmount = 27;
        public static readonly int StaminaRegenBonusAmount = 28;
        public static readonly int StaminaRegenBonusPercentage = 29;
        
        // Ability Related
        public static readonly int AttackRangeBonusAmount = 30;
        public static readonly int AttackRangeBonusPercentage = 31;
        public static readonly int AttackSizeBonusAmount = 32;
        public static readonly int AttackSizeBonusPercentage = 33;
        public static readonly int CooldownReductionBonusAmount = 34;
        public static readonly int CooldownReductionBonusPercentage = 35;
    }
}