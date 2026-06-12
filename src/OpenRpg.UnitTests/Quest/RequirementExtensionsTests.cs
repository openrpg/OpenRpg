using System;
using System.Collections.Generic;
using Moq;
using OpenRpg.Core.Associations;
using OpenRpg.Core.Requirements;
using OpenRpg.Entities.Entity.Variables;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Genres.Requirements;
using OpenRpg.Genres.Types;
using OpenRpg.Quests;
using OpenRpg.Quests.Extensions;
using OpenRpg.Quests.State;
using OpenRpg.Quests.Types;
using Xunit;

namespace OpenRpg.UnitTests.Quest;

public class RequirementExtensionsTests
{
    private readonly Mock<ICharacterRequirementChecker> _mockChecker;

    public RequirementExtensionsTests()
    {
        _mockChecker = new Mock<ICharacterRequirementChecker>();
    }

    [Fact]
    public void are_requirements_met_with_character_should_check_all_three()
    {
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<Character>(), It.IsAny<Requirement>())).Returns(true);
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<IReadOnlyList<QuestData>>(), It.IsAny<Requirement>())).Returns(true);
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<ITriggerState>(), It.IsAny<Requirement>())).Returns(true);

        var character = CreateCharacter();
        var requirements = new[]
        {
            new Requirement { RequirementType = GenreRequirementTypes.RaceRequirement, Association = new Association(1, 0) }
        };

        var result = _mockChecker.Object.AreRequirementsMet(character, requirements);

        Assert.True(result);
        _mockChecker.Verify(x => x.IsRequirementMet(character, It.IsAny<Requirement>()), Times.Once);
        var activeQuests = character.Variables.ActiveQuests;
        var triggerState = character.Variables.TriggerState;
        _mockChecker.Verify(x => x.IsRequirementMet(activeQuests, It.IsAny<Requirement>()), Times.Once);
        _mockChecker.Verify(x => x.IsRequirementMet(triggerState, It.IsAny<Requirement>()), Times.Once);
    }

    [Fact]
    public void are_requirements_met_with_character_should_return_false_when_character_check_fails()
    {
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<Character>(), It.IsAny<Requirement>())).Returns(false);
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<IReadOnlyList<QuestData>>(), It.IsAny<Requirement>())).Returns(true);
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<ITriggerState>(), It.IsAny<Requirement>())).Returns(true);

        var character = CreateCharacter();
        var requirements = new[]
        {
            new Requirement { RequirementType = GenreRequirementTypes.RaceRequirement, Association = new Association(1, 0) }
        };

        var result = _mockChecker.Object.AreRequirementsMet(character, requirements);

        Assert.False(result);
    }

    [Fact]
    public void are_requirements_met_with_character_should_return_false_when_quest_state_check_fails()
    {
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<Character>(), It.IsAny<Requirement>())).Returns(true);
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<IReadOnlyList<QuestData>>(), It.IsAny<Requirement>())).Returns(false);
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<ITriggerState>(), It.IsAny<Requirement>())).Returns(true);

        var character = CreateCharacter();
        var requirements = new[]
        {
            new Requirement { RequirementType = QuestRequirementTypes.QuestStateRequirement, Association = new Association(1, QuestStateTypes.QuestActive) }
        };

        var result = _mockChecker.Object.AreRequirementsMet(character, requirements);

        Assert.False(result);
    }

    [Fact]
    public void are_requirements_met_with_character_should_return_false_when_trigger_check_fails()
    {
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<Character>(), It.IsAny<Requirement>())).Returns(true);
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<IReadOnlyList<QuestData>>(), It.IsAny<Requirement>())).Returns(true);
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<ITriggerState>(), It.IsAny<Requirement>())).Returns(false);

        var character = CreateCharacter();
        var requirements = new[]
        {
            new Requirement { RequirementType = QuestRequirementTypes.TriggerRequirement, Association = new Association(1, 1) }
        };

        var result = _mockChecker.Object.AreRequirementsMet(character, requirements);

        Assert.False(result);
    }

    [Fact]
    public void are_requirements_met_with_character_should_return_true_when_all_pass()
    {
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<Character>(), It.IsAny<Requirement>())).Returns(true);
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<IReadOnlyList<QuestData>>(), It.IsAny<Requirement>())).Returns(true);
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<ITriggerState>(), It.IsAny<Requirement>())).Returns(true);

        var character = CreateCharacter();
        var requirements = new[]
        {
            new Requirement { RequirementType = QuestRequirementTypes.TriggerRequirement, Association = new Association(1, 1) }
        };

        var result = _mockChecker.Object.AreRequirementsMet(character, requirements);

        Assert.True(result);
    }

    [Fact]
    public void are_requirements_met_for_quest_state_should_check_all_requirements()
    {
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<IReadOnlyList<QuestData>>(), It.IsAny<Requirement>())).Returns(true);

        var quests = new List<QuestData>();
        var requirements = new[]
        {
            new Requirement { RequirementType = QuestRequirementTypes.QuestStateRequirement, Association = new Association(1, QuestStateTypes.QuestActive) },
            new Requirement { RequirementType = QuestRequirementTypes.QuestStateRequirement, Association = new Association(2, QuestStateTypes.QuestComplete) }
        };

        var result = _mockChecker.Object.AreRequirementsMet(quests, requirements);

        Assert.True(result);
        _mockChecker.Verify(x => x.IsRequirementMet(quests, It.IsAny<Requirement>()), Times.Exactly(2));
    }

    [Fact]
    public void are_requirements_met_for_quest_state_should_return_false_when_any_fails()
    {
        var callCount = 0;
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<IReadOnlyList<QuestData>>(), It.IsAny<Requirement>()))
            .Returns(() =>
            {
                callCount++;
                return callCount != 2;
            });

        var quests = new List<QuestData>();
        var requirements = new[]
        {
            new Requirement { RequirementType = QuestRequirementTypes.QuestStateRequirement, Association = new Association(1, QuestStateTypes.QuestActive) },
            new Requirement { RequirementType = QuestRequirementTypes.QuestStateRequirement, Association = new Association(2, QuestStateTypes.QuestComplete) }
        };

        var result = _mockChecker.Object.AreRequirementsMet(quests, requirements);

        Assert.False(result);
    }

    [Fact]
    public void are_requirements_met_for_trigger_state_should_check_all_requirements()
    {
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<ITriggerState>(), It.IsAny<Requirement>())).Returns(true);

        var triggerState = new TriggerState();
        var requirements = new[]
        {
            new Requirement { RequirementType = QuestRequirementTypes.TriggerRequirement, Association = new Association(1, 1) },
            new Requirement { RequirementType = QuestRequirementTypes.TriggerRequirement, Association = new Association(2, 1) }
        };

        var result = _mockChecker.Object.AreRequirementsMet(triggerState, requirements);

        Assert.True(result);
        _mockChecker.Verify(x => x.IsRequirementMet(triggerState, It.IsAny<Requirement>()), Times.Exactly(2));
    }

    [Fact]
    public void are_requirements_met_for_trigger_state_should_return_false_when_any_fails()
    {
        var callCount = 0;
        _mockChecker.Setup(x => x.IsRequirementMet(It.IsAny<ITriggerState>(), It.IsAny<Requirement>()))
            .Returns(() =>
            {
                callCount++;
                return callCount != 1;
            });

        var triggerState = new TriggerState();
        var requirements = new[]
        {
            new Requirement { RequirementType = QuestRequirementTypes.TriggerRequirement, Association = new Association(1, 1) },
            new Requirement { RequirementType = QuestRequirementTypes.TriggerRequirement, Association = new Association(2, 1) }
        };

        var result = _mockChecker.Object.AreRequirementsMet(triggerState, requirements);

        Assert.False(result);
    }

    [Fact]
    public void are_requirements_met_should_return_true_for_empty_requirements()
    {
        var character = CreateCharacter();
        var requirements = new Requirement[0];

        var result = _mockChecker.Object.AreRequirementsMet(character, requirements);

        Assert.True(result);
    }

    private static Character CreateCharacter()
    {
        var character = new Character
        {
            Variables = new EntityVariables()
        };
        var _ = character.Variables.ActiveQuests;
        var __ = character.Variables.TriggerState;
        return character;
    }
}
