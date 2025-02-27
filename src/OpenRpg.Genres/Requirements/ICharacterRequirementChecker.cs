using OpenRpg.Core.Requirements;
using OpenRpg.Entities.Requirements;
using OpenRpg.Genres.Characters;
using OpenRpg.Quests.State;

namespace OpenRpg.Genres.Requirements
{
    public interface ICharacterRequirementChecker : IEntityRequirementChecker<Character>
    {
        bool IsRequirementMet(IQuestState state, Requirement requirement);
        bool IsRequirementMet(ITriggerStateVariables state, Requirement requirement);
    }
}