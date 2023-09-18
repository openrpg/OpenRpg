using System.Collections.Generic;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests
{
    public class DefaultQuestState : IQuestState
    {
        public Dictionary<int, int> InternalQuestStates { get; set; } = new Dictionary<int, int>();

        public IReadOnlyDictionary<int, int> QuestStates => InternalQuestStates;
        
        public DefaultQuestState()
        {}
        
        public DefaultQuestState(Dictionary<int, int> questStates)
        { InternalQuestStates = questStates; }
        
        public int GetQuestState(int questId)
        {
            return InternalQuestStates.TryGetValue(questId, out var state) 
                ? state 
                : QuestStateTypes.QuestNotStarted;
        }

        public void SetQuestState(int questId, int questState)
        { InternalQuestStates[questId] = questState; }
    }
}