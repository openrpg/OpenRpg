using System.Collections.Generic;
using OpenRpg.Quests.State;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.Extensions;

public static class QuestStateExtensions
{
    public static int GetQuestState(this IQuestState questState, int questId)
    { return questState.GetValueOrDefault(questId, QuestStateTypes.QuestNotStarted); }
    
    public static int SetQuestState(this IQuestState questState, int questId, int newState)
    { return questState[questId] = newState; }
}