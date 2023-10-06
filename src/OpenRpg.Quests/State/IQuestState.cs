using OpenRpg.Core.Variables;

namespace OpenRpg.Quests.State
{
    public interface IQuestState : IVariables<int>
    {
        int GetQuestState(int questId);
    }
}