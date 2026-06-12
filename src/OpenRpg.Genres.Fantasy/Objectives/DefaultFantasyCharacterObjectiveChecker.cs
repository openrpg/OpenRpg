using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Objectives;
using OpenRpg.Quests;
using OpenRpg.Quests.Objectives;
using OpenRpg.Quests.State;

namespace OpenRpg.Genres.Fantasy.Objectives
{
    public class DefaultFantasyCharacterObjectiveChecker : DefaultCharacterObjectiveChecker
    {
        public override bool IsObjectiveMet(Character character, Objective objective)
        {
            return base.IsObjectiveMet(character, objective);
        }
    }
}
