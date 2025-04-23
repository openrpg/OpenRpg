namespace OpenRpg.Genres.Types
{
    public interface GenreEffectTypes
    {
        public static readonly int UnknownBonus = 0;
        
        // Pure Damage Types
        public static readonly int DamageBonusAmount = 1;
        public static readonly int DamageBonusPercentage = 2;
        public static readonly int CriticalRateBonusAmount = 3;
        public static readonly int CriticalRateBonusPercentage = 4;
        static readonly int CriticalDamageBonusAmount = 5;
        static readonly int CriticalDamageBonusPercentage = 6;
        
        // Pure Defense Types
        public static readonly int DefenseBonusAmount = 21;
        public static readonly int DefenseBonusPercentage = 22;
        
        // Attributes / Experience
        public static readonly int AllAttributeBonusAmount = 40;
        public static readonly int AllAttributeBonusPercentage = 41;
        public static readonly int ExperienceRestorePercentage = 42;
        public static readonly int ExperienceRestoreAmount = 43;
        
        // Vitals
        public static readonly int HealthBonusAmount = 60;
        public static readonly int HealthBonusPercentage = 61;
        public static readonly int HealthRestoreAmount = 62;
        public static readonly int HealthRestorePercentage = 63;
        public static readonly int HealthRegenBonusAmount = 64;
        public static readonly int HealthRegenBonusPercentage = 65;
        public static readonly int StaminaBonusAmount = 66;
        public static readonly int StaminaBonusPercentage = 67;
        public static readonly int StaminaRestoreAmount = 68;
        public static readonly int StaminaRestorePercentage = 69;
        public static readonly int StaminaRegenBonusAmount = 70;
        public static readonly int StaminaRegenBonusPercentage = 71;
        
        // Ability Related
        public static readonly int AttackRangeBonusAmount = 90;
        public static readonly int AttackRangeBonusPercentage = 91;
        public static readonly int AttackSizeBonusAmount = 92;
        public static readonly int AttackSizeBonusPercentage = 93;
        public static readonly int CooldownReductionBonusAmount = 94;
        public static readonly int CooldownReductionBonusPercentage = 95;
    }
}