using System.Linq;
using OpenRpg.Core.Requirements;
using OpenRpg.Entities.Requirements;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Requirements;
using OpenRpg.Quests.State;

namespace OpenRpg.Genres.Extensions
{
    public static class RequirementExtensions
    {
        public static bool AreRequirementsMet(this ICharacterRequirementChecker characterRequirementChecker, Character character, IHasRequirements hasRequirements)
        { return hasRequirements.Requirements.All(x => characterRequirementChecker.IsRequirementMet(character, x)); }
        
        public static bool AreRequirementsMet(this ICharacterRequirementChecker characterRequirementChecker, IQuestState questState, IHasRequirements hasRequirements)
        { return hasRequirements.Requirements.All(x => characterRequirementChecker.IsRequirementMet(questState, x)); }
        
        public static bool AreRequirementsMet(this ICharacterRequirementChecker characterRequirementChecker, ITriggerStateVariables triggerState, IHasRequirements hasRequirements)
        { return hasRequirements.Requirements.All(x => characterRequirementChecker.IsRequirementMet(triggerState, x)); }

        public static bool AreRequirementsMet(this ICharacterRequirementChecker characterRequirementChecker, IQuestState questState, ITriggerStateVariables triggerState,
            Character character, IHasRequirements hasRequirements)
        {
            return AreRequirementsMet(characterRequirementChecker, character, hasRequirements) &&
                   AreRequirementsMet(characterRequirementChecker, questState, hasRequirements) &&
                   AreRequirementsMet(characterRequirementChecker, triggerState, hasRequirements);
        }
    }
}