using System.Collections.Generic;
using OpenRpg.Core.Requirements;
using OpenRpg.Entities.Requirements;
using OpenRpg.Genres.Characters;
using OpenRpg.Quests;
using OpenRpg.Quests.State;

namespace OpenRpg.Genres.Requirements
{
    public interface ICharacterRequirementChecker : IEntityRequirementChecker<Character>
    {
        bool IsRequirementMet(IReadOnlyList<QuestData> quests, Requirement requirement);
        bool IsRequirementMet(ITriggerState state, Requirement requirement);
    }
}
