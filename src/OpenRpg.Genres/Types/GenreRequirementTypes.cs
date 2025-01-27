namespace OpenRpg.Genres.Types
{
    public interface GenreRequirementTypes
    {
        public static readonly int UnknownRequirement = 0;
        
        public static readonly int RaceRequirement = 1;
        public static readonly int ClassRequirement = 2;
        public static readonly int TriggerRequirement = 4;
        public static readonly int QuestStateRequirement = 5;
        public static readonly int InventoryItemRequirement = 6;
        public static readonly int EquipmentItemRequirement = 7;
        public static readonly int GenderRequirement = 8;
        public static readonly int ActiveEffectRequirement = 9;
        public static readonly int MaxHealthRequirement = 10;
        public static readonly int TradeSkillRequirement = 11;
    }
}