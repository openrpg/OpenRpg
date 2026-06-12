using System.Collections.Generic;
using System.Linq;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Objectives;
using OpenRpg.Quests;
using OpenRpg.Quests.Extensions;
using OpenRpg.Quests.State;

namespace OpenRpg.Genres.Extensions
{
    public static class ObjectiveExtensions
    {
        public static bool AreObjectivesMet(this ICharacterObjectiveChecker checker, Character character, QuestTemplate quest, ObjectiveState objectiveState)
        {
            return quest.Objectives.Select((obj, index) => new { obj, index }).All(x =>
            {
                if (!checker.IsObjectiveMet(character, x.obj)) return false;
                if (!checker.IsObjectiveMet(character.Variables.ActiveQuests, x.obj)) return false;
                if (!checker.IsObjectiveMet(character.Variables.TriggerState, x.obj)) return false;
                if (!checker.IsObjectiveMet(objectiveState, x.obj, quest.Id, x.index)) return false;
                return true;
            });
        }

        public static bool AreObjectivesMet(this ICharacterObjectiveChecker checker, Character character, QuestTemplate quest, QuestData questData)
        {
            return checker.AreObjectivesMet(character, quest, questData.ObjectiveState);
        }
    }
}
