using OpenRpg.Entities.Types;

namespace OpenRpg.Quests.Types
{
    public interface QuestVariableTypes : CoreVariableTypes
    {
        public static int QuestVariables = 30;
        public static int QuestStateVariables = 31;
        public static int TriggerVariables = 32;
        public static int FactionVariables = 33;
        public static int FactionReputationVariables = 34;
    }
}