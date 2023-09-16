using System.Collections.Generic;

namespace OpenRpg.Quests
{
    public interface IQuestState
    {
        IReadOnlyDictionary<int,int> QuestStates { get; }
        
        int GetQuestState(int questId);
        void SetQuestState(int questId, int questState);
    }
}