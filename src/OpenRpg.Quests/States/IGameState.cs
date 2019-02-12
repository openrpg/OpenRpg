using System.Collections.Generic;

namespace OpenRpg.Quests.States
{
    public interface IGameState
    {
        IDictionary<int, bool> Triggers { get; }
        IDictionary<int, int> QuestStates { get; }
    }
}