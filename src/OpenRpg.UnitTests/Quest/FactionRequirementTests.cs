using OpenRpg.Core.Associations;
using OpenRpg.Core.Requirements;
using OpenRpg.Entities.Entity.Variables;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Requirements;
using OpenRpg.Quests.Extensions;
using OpenRpg.Quests.Factions;
using OpenRpg.Quests.Types;
using Xunit;

namespace OpenRpg.UnitTests.Quest;

public class FactionRequirementTests
{
    private readonly DefaultCharacterRequirementChecker _checker = new();

    [Fact]
    public void faction_requirement_should_pass_when_reputation_meets_threshold()
    {
        var character = CreateCharacterWithFactionReputation(1, 50);
        var requirement = MakeRequirement(1, 30);

        var result = _checker.IsRequirementMet(character, requirement);

        Assert.True(result);
    }

    [Fact]
    public void faction_requirement_should_fail_when_reputation_below_threshold()
    {
        var character = CreateCharacterWithFactionReputation(1, 10);
        var requirement = MakeRequirement(1, 50);

        var result = _checker.IsRequirementMet(character, requirement);

        Assert.False(result);
    }

    [Fact]
    public void faction_requirement_should_fail_when_no_faction_reputation_container()
    {
        var character = new Character { Variables = new EntityVariables() };
        var requirement = MakeRequirement(1, 30);

        var result = _checker.IsRequirementMet(character, requirement);

        Assert.False(result);
    }

    [Fact]
    public void faction_requirement_should_fail_when_faction_not_present_in_reputation()
    {
        var character = new Character { Variables = new EntityVariables() };
        character.Variables.FactionReputation[2] = 100;
        var requirement = MakeRequirement(1, 30);

        var result = _checker.IsRequirementMet(character, requirement);

        Assert.False(result);
    }

    [Fact]
    public void faction_requirement_should_pass_when_reputation_is_exact_match()
    {
        var character = CreateCharacterWithFactionReputation(1, 50);
        var requirement = MakeRequirement(1, 50);

        var result = _checker.IsRequirementMet(character, requirement);

        Assert.True(result);
    }

    private static Character CreateCharacterWithFactionReputation(int factionId, int reputation)
    {
        var character = new Character { Variables = new EntityVariables() };
        character.Variables.FactionReputation[factionId] = reputation;
        return character;
    }

    private static Requirement MakeRequirement(int factionId, int minimumReputation)
    {
        return new Requirement
        {
            RequirementType = QuestRequirementTypes.FactionStateRequirement,
            Association = new Association(factionId, minimumReputation)
        };
    }
}
