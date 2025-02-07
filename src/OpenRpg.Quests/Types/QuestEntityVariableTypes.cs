using OpenRpg.Entities.Types;

namespace OpenRpg.Quests.Types
{
    public interface QuestEntityVariableTypes : EntityVariableTypes
    {
        public static int FactionReputation = 40;
        public static int QuestState = 41;
        public static int TriggerState = 42;
    }
}