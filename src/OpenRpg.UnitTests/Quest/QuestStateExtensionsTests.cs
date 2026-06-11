using OpenRpg.Quests.Extensions;
using OpenRpg.Quests.State;
using OpenRpg.Quests.Types;
using Xunit;

namespace OpenRpg.UnitTests.Quest;

public class QuestStateExtensionsTests
{
    [Fact]
    public void get_quest_state_should_return_not_started_when_no_state_set()
    {
        var questState = new QuestState();
        var state = questState.GetQuestState(1);
        Assert.Equal(QuestStateTypes.QuestNotStarted, state);
    }

    [Fact]
    public void set_quest_state_should_store_and_return_new_state()
    {
        var questState = new QuestState();
        var result = questState.SetQuestState(1, QuestStateTypes.QuestActive);
        Assert.Equal(QuestStateTypes.QuestActive, result);
        Assert.Equal(QuestStateTypes.QuestActive, questState.GetQuestState(1));
    }

    [Fact]
    public void get_quest_state_should_return_set_state()
    {
        var questState = new QuestState();
        questState.SetQuestState(1, QuestStateTypes.QuestComplete);
        Assert.Equal(QuestStateTypes.QuestComplete, questState.GetQuestState(1));
    }

    [Fact]
    public void set_quest_state_should_overwrite_existing_state()
    {
        var questState = new QuestState();
        questState.SetQuestState(1, QuestStateTypes.QuestActive);
        questState.SetQuestState(1, QuestStateTypes.QuestComplete);
        Assert.Equal(QuestStateTypes.QuestComplete, questState.GetQuestState(1));
    }

    [Fact]
    public void should_track_multiple_quests_independently()
    {
        var questState = new QuestState();
        questState.SetQuestState(1, QuestStateTypes.QuestActive);
        questState.SetQuestState(2, QuestStateTypes.QuestComplete);

        Assert.Equal(QuestStateTypes.QuestActive, questState.GetQuestState(1));
        Assert.Equal(QuestStateTypes.QuestComplete, questState.GetQuestState(2));
        Assert.Equal(QuestStateTypes.QuestNotStarted, questState.GetQuestState(3));
    }

    [Fact]
    public void get_quest_state_should_return_default_for_unset_quest()
    {
        var questState = new QuestState();
        Assert.Equal(QuestStateTypes.QuestNotStarted, questState.GetQuestState(999));
    }
}
