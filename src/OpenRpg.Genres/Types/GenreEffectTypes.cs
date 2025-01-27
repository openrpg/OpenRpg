namespace OpenRpg.Genres.Types
{
    public interface GenreEffectTypes
    {
        public static readonly int UnknownBonus = 0;
        
        // Pure Damage Types
        public static readonly int DamageBonusAmount = 1;
        public static readonly int DamageBonusPercentage = 2;
        
        // Pure Defense Types
        public static readonly int DefenseBonusAmount = 10;
        public static readonly int DefenseBonusPercentage = 11;
        
        // All Attributes
        public static readonly int AllAttributeBonusAmount = 20;
        public static readonly int AllAttributeBonusPercentage = 21;
    }
}