using OpenRpg.Core.Requirements;
using OpenRpg.Genres.Fantasy.Characters;
using OpenRpg.Genres.Fantasy.Player;

namespace OpenRpg.Genres.Fantasy.Requirements
{
    public interface IRequirementChecker
    {
        bool IsRequirementMet(ICharacter character, Requirement requirement);
        bool IsRequirementMet(IPlayerState player, Requirement requirement);
    }
}