using System.Collections.Generic;
using System.Linq;
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

        public virtual bool IsObjectiveMet(IReadOnlyList<QuestData> quests, Objective objective)
        {
            if (objective.ObjectiveType == ObjectiveTypes.QuestObjective)
            {
                var questData = quests.FirstOrDefault(q => q.TemplateId == objective.Association.AssociatedId);
                return questData?.State == QuestStateTypes.QuestComplete;
            }

            return true;
        }

        public virtual bool IsObjectiveMet(ITriggerState state, Objective objective)
        {
            if (objective.ObjectiveType == ObjectiveTypes.TriggerObjective)
            { return state.HasTriggered(objective.Association.AssociatedId); }

            return true;
        }

        public virtual bool IsObjectiveMet(ObjectiveState state, Objective objective, int questId, int objectiveIndex)
        {
            var key = (questId << 16) | (objectiveIndex & 0xFFFF);
            var progress = state.ContainsKey(key) ? state[key] : 0;

            if (objective.ObjectiveType == ObjectiveTypes.ItemObjective)
            { return progress >= objective.Association.AssociatedValue; }

            if (objective.ObjectiveType == GenresObjectiveTypes.EnemyDefeatedObjective)
            { return progress >= objective.Association.AssociatedValue; }

            if (objective.ObjectiveType == GenresObjectiveTypes.EnemySightedObjective)
            { return progress >= objective.Association.AssociatedValue; }

            if (objective.ObjectiveType == GenresObjectiveTypes.CurrencyObjective)
            { return progress >= objective.Association.AssociatedValue; }

            return true;
        }
    }
}
