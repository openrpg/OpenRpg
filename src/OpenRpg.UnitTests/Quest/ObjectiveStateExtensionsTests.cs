using OpenRpg.Quests.Extensions;
using OpenRpg.Quests.State;
using Xunit;

namespace OpenRpg.UnitTests.Quest;

public class ObjectiveStateExtensionsTests
{
    private const int QuestId = 10;
    private const int ObjectiveIndex = 0;

    [Fact]
    public void get_objective_progress_should_return_zero_when_no_progress_set()
    {
        var state = new ObjectiveState();
        var progress = state.GetObjectiveProgress(QuestId, ObjectiveIndex);
        Assert.Equal(0, progress);
    }

    [Fact]
    public void set_objective_progress_should_store_and_retrieve_value()
    {
        var state = new ObjectiveState();
        state.SetObjectiveProgress(QuestId, ObjectiveIndex, 5);
        Assert.Equal(5, state.GetObjectiveProgress(QuestId, ObjectiveIndex));
    }

    [Fact]
    public void add_objective_progress_should_increment_existing_progress()
    {
        var state = new ObjectiveState();
        state.SetObjectiveProgress(QuestId, ObjectiveIndex, 3);
        state.AddObjectiveProgress(QuestId, ObjectiveIndex, 2);
        Assert.Equal(5, state.GetObjectiveProgress(QuestId, ObjectiveIndex));
    }

    [Fact]
    public void add_objective_progress_should_start_from_zero_if_not_set()
    {
        var state = new ObjectiveState();
        state.AddObjectiveProgress(QuestId, ObjectiveIndex, 4);
        Assert.Equal(4, state.GetObjectiveProgress(QuestId, ObjectiveIndex));
    }

    [Fact]
    public void add_objective_progress_should_accumulate_multiple_adds()
    {
        var state = new ObjectiveState();
        state.AddObjectiveProgress(QuestId, ObjectiveIndex, 1);
        state.AddObjectiveProgress(QuestId, ObjectiveIndex, 1);
        state.AddObjectiveProgress(QuestId, ObjectiveIndex, 1);
        Assert.Equal(3, state.GetObjectiveProgress(QuestId, ObjectiveIndex));
    }

    [Fact]
    public void is_objective_complete_should_return_false_when_below_threshold()
    {
        var state = new ObjectiveState();
        state.SetObjectiveProgress(QuestId, ObjectiveIndex, 2);
        Assert.False(state.IsObjectiveComplete(QuestId, ObjectiveIndex, 5));
    }

    [Fact]
    public void is_objective_complete_should_return_true_when_at_threshold()
    {
        var state = new ObjectiveState();
        state.SetObjectiveProgress(QuestId, ObjectiveIndex, 5);
        Assert.True(state.IsObjectiveComplete(QuestId, ObjectiveIndex, 5));
    }

    [Fact]
    public void is_objective_complete_should_return_true_when_above_threshold()
    {
        var state = new ObjectiveState();
        state.SetObjectiveProgress(QuestId, ObjectiveIndex, 10);
        Assert.True(state.IsObjectiveComplete(QuestId, ObjectiveIndex, 5));
    }

    [Fact]
    public void clear_quest_objectives_should_remove_all_objectives_for_quest()
    {
        var state = new ObjectiveState();
        state.SetObjectiveProgress(QuestId, 0, 3);
        state.SetObjectiveProgress(QuestId, 1, 5);
        state.SetObjectiveProgress(QuestId, 2, 1);

        state.ClearQuestObjectives(QuestId, 3);

        Assert.Equal(0, state.GetObjectiveProgress(QuestId, 0));
        Assert.Equal(0, state.GetObjectiveProgress(QuestId, 1));
        Assert.Equal(0, state.GetObjectiveProgress(QuestId, 2));
    }

    [Fact]
    public void clear_quest_objectives_should_not_affect_other_quests()
    {
        var state = new ObjectiveState();
        state.SetObjectiveProgress(QuestId, 0, 3);
        state.SetObjectiveProgress(99, 0, 7);

        state.ClearQuestObjectives(QuestId, 1);

        Assert.Equal(0, state.GetObjectiveProgress(QuestId, 0));
        Assert.Equal(7, state.GetObjectiveProgress(99, 0));
    }

    [Fact]
    public void should_track_multiple_quests_and_objectives_independently()
    {
        var state = new ObjectiveState();
        state.AddObjectiveProgress(10, 0, 2);
        state.AddObjectiveProgress(10, 1, 5);
        state.AddObjectiveProgress(20, 0, 8);

        Assert.Equal(2, state.GetObjectiveProgress(10, 0));
        Assert.Equal(5, state.GetObjectiveProgress(10, 1));
        Assert.Equal(8, state.GetObjectiveProgress(20, 0));
        Assert.Equal(0, state.GetObjectiveProgress(20, 1));
        Assert.Equal(0, state.GetObjectiveProgress(30, 0));
    }
}
