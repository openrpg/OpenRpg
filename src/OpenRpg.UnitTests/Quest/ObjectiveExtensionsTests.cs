using Moq;
using OpenRpg.Core.Associations;
using OpenRpg.Entities.Entity.Variables;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Objectives;
using OpenRpg.Quests;
using OpenRpg.Quests.Extensions;
using OpenRpg.Quests.Objectives;
using OpenRpg.Quests.State;
using Xunit;

namespace OpenRpg.UnitTests.Quest;

public class ObjectiveExtensionsTests
{
    private readonly Mock<ICharacterObjectiveChecker> _mockChecker;

    public ObjectiveExtensionsTests()
    {
        _mockChecker = new Mock<ICharacterObjectiveChecker>();
    }

    [Fact]
    public void are_objectives_met_should_check_all_four_contexts()
    {
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<Character>(), It.IsAny<Objective>())).Returns(true);
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<IQuestState>(), It.IsAny<Objective>())).Returns(true);
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<ITriggerState>(), It.IsAny<Objective>())).Returns(true);
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<IObjectiveState>(), It.IsAny<Objective>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);

        var character = CreateCharacter();
        var quest = CreateQuest(new[] { new Objective { ObjectiveType = 1, Association = new Association(1, 1) } });
        var questData = new QuestData(quest);

        var result = _mockChecker.Object.AreObjectivesMet(character, questData);

        Assert.True(result);
        _mockChecker.Verify(x => x.IsObjectiveMet(character, It.IsAny<Objective>()), Times.Once);
        var questState = character.Variables.QuestState;
        var triggerState = character.Variables.TriggerState;
        _mockChecker.Verify(x => x.IsObjectiveMet(questState, It.IsAny<Objective>()), Times.Once);
        _mockChecker.Verify(x => x.IsObjectiveMet(triggerState, It.IsAny<Objective>()), Times.Once);
        _mockChecker.Verify(x => x.IsObjectiveMet(questData.ObjectiveState, It.IsAny<Objective>(), quest.Id, 0), Times.Once);
    }

    [Fact]
    public void are_objectives_met_should_return_false_when_character_check_fails()
    {
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<Character>(), It.IsAny<Objective>())).Returns(false);
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<IQuestState>(), It.IsAny<Objective>())).Returns(true);
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<ITriggerState>(), It.IsAny<Objective>())).Returns(true);
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<IObjectiveState>(), It.IsAny<Objective>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);

        var character = CreateCharacter();
        var quest = CreateQuest(new[] { new Objective { ObjectiveType = 1, Association = new Association(1, 1) } });
        var questData = new QuestData(quest);

        var result = _mockChecker.Object.AreObjectivesMet(character, questData);

        Assert.False(result);
    }

    [Fact]
    public void are_objectives_met_should_return_false_when_quest_state_check_fails()
    {
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<Character>(), It.IsAny<Objective>())).Returns(true);
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<IQuestState>(), It.IsAny<Objective>())).Returns(false);
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<ITriggerState>(), It.IsAny<Objective>())).Returns(true);
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<IObjectiveState>(), It.IsAny<Objective>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);

        var character = CreateCharacter();
        var quest = CreateQuest(new[] { new Objective { ObjectiveType = 1, Association = new Association(1, 1) } });
        var questData = new QuestData(quest);

        var result = _mockChecker.Object.AreObjectivesMet(character, questData);

        Assert.False(result);
    }

    [Fact]
    public void are_objectives_met_should_return_false_when_trigger_check_fails()
    {
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<Character>(), It.IsAny<Objective>())).Returns(true);
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<IQuestState>(), It.IsAny<Objective>())).Returns(true);
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<ITriggerState>(), It.IsAny<Objective>())).Returns(false);
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<IObjectiveState>(), It.IsAny<Objective>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);

        var character = CreateCharacter();
        var quest = CreateQuest(new[] { new Objective { ObjectiveType = 1, Association = new Association(1, 1) } });
        var questData = new QuestData(quest);

        var result = _mockChecker.Object.AreObjectivesMet(character, questData);

        Assert.False(result);
    }

    [Fact]
    public void are_objectives_met_should_return_false_when_objective_state_check_fails()
    {
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<Character>(), It.IsAny<Objective>())).Returns(true);
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<IQuestState>(), It.IsAny<Objective>())).Returns(true);
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<ITriggerState>(), It.IsAny<Objective>())).Returns(true);
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<IObjectiveState>(), It.IsAny<Objective>(), It.IsAny<int>(), It.IsAny<int>())).Returns(false);

        var character = CreateCharacter();
        var quest = CreateQuest(new[] { new Objective { ObjectiveType = 1, Association = new Association(1, 1) } });
        var questData = new QuestData(quest);

        var result = _mockChecker.Object.AreObjectivesMet(character, questData);

        Assert.False(result);
    }

    [Fact]
    public void are_objectives_met_should_return_true_when_all_pass()
    {
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<Character>(), It.IsAny<Objective>())).Returns(true);
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<IQuestState>(), It.IsAny<Objective>())).Returns(true);
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<ITriggerState>(), It.IsAny<Objective>())).Returns(true);
        _mockChecker.Setup(x => x.IsObjectiveMet(It.IsAny<IObjectiveState>(), It.IsAny<Objective>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);

        var character = CreateCharacter();
        var quest = CreateQuest(new[]
        {
            new Objective { ObjectiveType = 1, Association = new Association(1, 1) },
            new Objective { ObjectiveType = 2, Association = new Association(2, 5) }
        });
        var questData = new QuestData(quest);

        var result = _mockChecker.Object.AreObjectivesMet(character, questData);

        Assert.True(result);
    }

    [Fact]
    public void are_objectives_met_should_return_true_for_empty_objectives()
    {
        var character = CreateCharacter();
        var quest = CreateQuest(System.Array.Empty<Objective>());
        var questData = new QuestData(quest);

        var result = _mockChecker.Object.AreObjectivesMet(character, questData);

        Assert.True(result);
    }

    private static Character CreateCharacter()
    {
        var character = new Character
        {
            Variables = new EntityVariables()
        };
        var _ = character.Variables.QuestState;
        var __ = character.Variables.TriggerState;
        return character;
    }

    private static OpenRpg.Quests.Quest CreateQuest(Objective[] objectives)
    {
        return new OpenRpg.Quests.Quest
        {
            Id = 1,
            NameLocaleId = "Test Quest",
            DescriptionLocaleId = "A test quest",
            Objectives = objectives,
            Variables = new OpenRpg.Quests.Variables.QuestVariables()
        };
    }
}
