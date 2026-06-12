using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Requirements;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Requirements;
using OpenRpg.Quests;
using OpenRpg.Quests.Extensions;
using OpenRpg.Quests.State;

namespace OpenRpg.Genres.Extensions
{
    public static class RequirementExtensions
    {
        public static bool AreRequirementsMet(this ICharacterRequirementChecker characterRequirementChecker, IReadOnlyList<QuestData> quests, IReadOnlyCollection<Requirement> hasRequirements)
        { return hasRequirements.All(x => characterRequirementChecker.IsRequirementMet(quests, x)); }
        
        public static bool AreRequirementsMet(this ICharacterRequirementChecker characterRequirementChecker, ITriggerState triggerState, IReadOnlyCollection<Requirement> hasRequirements)
        { return hasRequirements.All(x => characterRequirementChecker.IsRequirementMet(triggerState, x)); }

        public static bool AreRequirementsMet(this ICharacterRequirementChecker characterRequirementChecker, Character character, IReadOnlyCollection<Requirement> hasRequirements)
        {
            return hasRequirements.All(x =>
            {
                if (!characterRequirementChecker.IsRequirementMet(character, x))
                { return false; }

                if (!characterRequirementChecker.IsRequirementMet(character.Variables.ActiveQuests, x))
                { return false; }

                if (!characterRequirementChecker.IsRequirementMet(character.Variables.TriggerState, x))
                { return false; }

                return true;
            });
        }
    }
}