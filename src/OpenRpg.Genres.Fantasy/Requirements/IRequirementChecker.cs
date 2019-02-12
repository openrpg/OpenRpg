using OpenRpg.Core.Requirements;
using OpenRpg.Genres.Fantasy.Characters;
using OpenRpg.Quests.States;

namespace OpenRpg.Genres.Fantasy.Requirements
{
    public interface IRequirementChecker
    {
        bool IsRequirementMet(ICharacter character, Requirement requirement);
        bool IsRequirementMet(IGameState state, Requirement requirement);
    }
}