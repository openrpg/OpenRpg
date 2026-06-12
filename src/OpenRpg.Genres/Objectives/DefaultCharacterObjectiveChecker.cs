using OpenRpg.Combat.Extensions;
using OpenRpg.Core.Requirements;
using OpenRpg.Entities.Extensions;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Types;
using OpenRpg.Quests;
using OpenRpg.Quests.Extensions;
using OpenRpg.Quests.Objectives;
using OpenRpg.Quests.State;
using OpenRpg.Quests.Types;

namespace OpenRpg.Genres.Objectives
{
    public class DefaultCharacterObjectiveChecker : ICharacterObjectiveChecker
    {
        public virtual bool IsObjectiveMet(Character character, Objective objective)
        {
            if (objective.ObjectiveType == ObjectiveTypes.LevelObjective)
            {
                if (!character.Variables.HasClass()) { return false; }
                return character.Variables.Class.Variables.Level >= objective.Association.AssociatedValue;
            }

            if (objective.ObjectiveType == ObjectiveTypes.ClassObjective)
            {
                if (!character.Variables.HasClass()) { return false; }
                return character.Variables.Class.TemplateId == objective.Association.AssociatedId;
            }

            if (objective.ObjectiveType == ObjectiveTypes.EffectObjective)
            {
                if (!character.Variables.HasActiveEffects()) { return false; }
                return character.Variables.ActiveEffects.HasEffect(objective.Association.AssociatedId);
            }

            return true;
        }

        public virtual bool IsObjectiveMet(IQuestState state, Objective objective)
        {
            if (objective.ObjectiveType == ObjectiveTypes.QuestObjective)
            { return state.GetQuestState(objective.Association.AssociatedId) == QuestStateTypes.QuestComplete; }

            return true;
        }

        public virtual bool IsObjectiveMet(ITriggerState state, Objective objective)
        {
            if (objective.ObjectiveType == ObjectiveTypes.TriggerObjective)
            { return state.HasTriggered(objective.Association.AssociatedId); }

            return true;
        }

        public virtual bool IsObjectiveMet(IObjectiveState state, Objective objective, int questId, int objectiveIndex)
        {
            if (objective.ObjectiveType == ObjectiveTypes.ItemObjective)
            { return state.IsObjectiveComplete(questId, objectiveIndex, objective.Association.AssociatedValue); }

            if (objective.ObjectiveType == GenresObjectiveTypes.EnemyDefeatedObjective)
            { return state.IsObjectiveComplete(questId, objectiveIndex, objective.Association.AssociatedValue); }

            if (objective.ObjectiveType == GenresObjectiveTypes.EnemySightedObjective)
            { return state.IsObjectiveComplete(questId, objectiveIndex, objective.Association.AssociatedValue); }

            if (objective.ObjectiveType == GenresObjectiveTypes.CurrencyObjective)
            { return state.IsObjectiveComplete(questId, objectiveIndex, objective.Association.AssociatedValue); }

            return true;
        }
    }
}
