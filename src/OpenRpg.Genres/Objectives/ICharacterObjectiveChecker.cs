using OpenRpg.Genres.Characters;
using OpenRpg.Quests;
using OpenRpg.Quests.Objectives;
using OpenRpg.Quests.State;

namespace OpenRpg.Genres.Objectives
{
    public interface ICharacterObjectiveChecker : IObjectiveChecker<Character>
    {
        bool IsObjectiveMet(IQuestState state, Objective objective);
        bool IsObjectiveMet(ITriggerState state, Objective objective);
        bool IsObjectiveMet(IObjectiveState state, Objective objective, int questId, int objectiveIndex);
    }
}
