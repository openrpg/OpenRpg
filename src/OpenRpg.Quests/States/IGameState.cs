using System.Collections.Generic;
using OpenRpg.Core.Variables;

namespace OpenRpg.Quests.States
{
    public interface IGameState
    {
        IVariables<bool> Triggers { get; }
        IVariables<int> QuestStates { get; }
    }
}