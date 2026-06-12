using OpenRpg.Quests;
using Xunit;

namespace OpenRpg.UnitTests.Quest;

public class QuestDataTests
{
    [Fact]
    public void get_objective_progress_should_return_zero_when_no_progress_set()
    {
        var questData = new QuestData { TemplateId = 10 };
        var progress = questData.GetObjectiveProgress(0);
        Assert.Equal(0, progress);
    }

    [Fact]
    public void add_objective_progress_should_store_and_retrieve_value()
    {
        var questData = new QuestData { TemplateId = 10 };
        questData.AddObjectiveProgress(0, 5);
        Assert.Equal(5, questData.GetObjectiveProgress(0));
    }

    [Fact]
    public void add_objective_progress_should_accumulate_multiple_adds()
    {
        var questData = new QuestData { TemplateId = 10 };
        questData.AddObjectiveProgress(0, 1);
        questData.AddObjectiveProgress(0, 1);
        questData.AddObjectiveProgress(0, 1);
        Assert.Equal(3, questData.GetObjectiveProgress(0));
    }

    [Fact]
    public void is_objective_complete_should_return_false_when_below_threshold()
    {
        var questData = new QuestData { TemplateId = 10 };
        questData.AddObjectiveProgress(0, 2);
        Assert.False(questData.IsObjectiveComplete(0, 5));
    }

    [Fact]
    public void is_objective_complete_should_return_true_when_at_threshold()
    {
        var questData = new QuestData { TemplateId = 10 };
        questData.AddObjectiveProgress(0, 5);
        Assert.True(questData.IsObjectiveComplete(0, 5));
    }

    [Fact]
    public void is_objective_complete_should_return_true_when_above_threshold()
    {
        var questData = new QuestData { TemplateId = 10 };
        questData.AddObjectiveProgress(0, 10);
        Assert.True(questData.IsObjectiveComplete(0, 5));
    }

    [Fact]
    public void clear_objectives_should_remove_all_objectives()
    {
        var questData = new QuestData { TemplateId = 10 };
        questData.AddObjectiveProgress(0, 3);
        questData.AddObjectiveProgress(1, 5);
        questData.AddObjectiveProgress(2, 1);

        questData.ClearObjectives(3);

        Assert.Equal(0, questData.GetObjectiveProgress(0));
        Assert.Equal(0, questData.GetObjectiveProgress(1));
        Assert.Equal(0, questData.GetObjectiveProgress(2));
    }

    [Fact]
    public void clear_objectives_should_not_affect_other_objectives()
    {
        var questData = new QuestData { TemplateId = 10 };
        questData.AddObjectiveProgress(0, 3);
        questData.AddObjectiveProgress(1, 5);

        questData.ClearObjectives(1);

        Assert.Equal(0, questData.GetObjectiveProgress(0));
        Assert.Equal(5, questData.GetObjectiveProgress(1));
    }

    [Fact]
    public void objectives_should_use_composite_key_based_on_template_id()
    {
        var quest1 = new QuestData { TemplateId = 10 };
        var quest2 = new QuestData { TemplateId = 20 };

        quest1.AddObjectiveProgress(0, 3);
        quest2.AddObjectiveProgress(0, 7);

        Assert.Equal(3, quest1.GetObjectiveProgress(0));
        Assert.Equal(7, quest2.GetObjectiveProgress(0));
    }

    [Fact]
    public void multiple_objectives_within_same_quest_should_be_independent()
    {
        var questData = new QuestData { TemplateId = 10 };
        questData.AddObjectiveProgress(0, 2);
        questData.AddObjectiveProgress(1, 5);
        questData.AddObjectiveProgress(2, 8);

        Assert.Equal(2, questData.GetObjectiveProgress(0));
        Assert.Equal(5, questData.GetObjectiveProgress(1));
        Assert.Equal(8, questData.GetObjectiveProgress(2));
        Assert.Equal(0, questData.GetObjectiveProgress(3));
    }

    [Fact]
    public void default_state_should_be_not_started()
    {
        var questData = new QuestData { TemplateId = 10 };
        Assert.Equal(1, questData.State);
    }
}
