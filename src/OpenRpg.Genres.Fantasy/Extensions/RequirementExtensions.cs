using System.Linq;
using OpenRpg.Core.Requirements;
using OpenRpg.Genres.Fantasy.Characters;
using OpenRpg.Genres.Fantasy.Requirements;
using OpenRpg.Quests.States;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class RequirementExtensions
    {
        public static bool AreRequirementsMet(this IRequirementChecker requirementChecker, ICharacter character, IHasRequirements hasRequirements)
        { return hasRequirements.Requirements.All(x => requirementChecker.IsRequirementMet(character, x)); }
        
        public static bool AreRequirementsMet(this IRequirementChecker requirementChecker, IGameState state, IHasRequirements hasRequirements)
        { return hasRequirements.Requirements.All(x => requirementChecker.IsRequirementMet(state, x)); }

        public static bool AreRequirementsMet(this IRequirementChecker requirementChecker, IGameState state,
            ICharacter character, IHasRequirements hasRequirements)
        {
            var characterRequirementsMet = AreRequirementsMet(requirementChecker, character, hasRequirements);
            if(characterRequirementsMet == false) { return false; }

            return AreRequirementsMet(requirementChecker, state, hasRequirements);
        }
    }
}