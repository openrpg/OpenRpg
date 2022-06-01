using OpenRpg.Core.Variables;

namespace OpenRpg.Quests.States
{
    public interface IQuestStates
    {
        IVariables<bool> Triggers { get; }
        IVariables<int> QuestStates { get; }
    }
}