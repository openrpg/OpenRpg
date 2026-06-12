using System.Collections.Generic;
using OpenRpg.Quests.State;

namespace OpenRpg.Quests.Extensions;

public static class ObjectiveStateExtensions
{
    public static int MakeObjectiveKey(int questId, int objectiveIndex)
    { return (questId << 16) | (objectiveIndex & 0xFFFF); }

    public static int GetObjectiveProgress(this IObjectiveState objectiveState, int questId, int objectiveIndex)
    { return objectiveState.GetValueOrDefault(MakeObjectiveKey(questId, objectiveIndex), 0); }

    public static void SetObjectiveProgress(this IObjectiveState objectiveState, int questId, int objectiveIndex, int value)
    { objectiveState[MakeObjectiveKey(questId, objectiveIndex)] = value; }

    public static void AddObjectiveProgress(this IObjectiveState objectiveState, int questId, int objectiveIndex, int amount)
    {
        var key = MakeObjectiveKey(questId, objectiveIndex);
        objectiveState[key] = objectiveState.GetValueOrDefault(key, 0) + amount;
    }

    public static bool IsObjectiveComplete(this IObjectiveState objectiveState, int questId, int objectiveIndex, int requiredAmount)
    { return objectiveState.GetObjectiveProgress(questId, objectiveIndex) >= requiredAmount; }

    public static void ClearQuestObjectives(this IObjectiveState objectiveState, int questId, int objectiveCount)
    {
        for (var i = 0; i < objectiveCount; i++)
        {
            var key = MakeObjectiveKey(questId, i);
            if (objectiveState.ContainsKey(key))
            { objectiveState.Remove(key); }
        }
    }
}
