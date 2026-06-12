using System.Collections.Generic;
using OpenRpg.Entities.Entity.Variables;
using OpenRpg.Quests;
using OpenRpg.Quests.Extensions;
using OpenRpg.Quests.Factions;
using OpenRpg.Quests.State;
using Xunit;

namespace OpenRpg.UnitTests.Quest;

public class QuestEntityVariableExtensionTests
{
    [Fact]
    public void should_correctly_handle_faction_reputation_on_entity()
    {
        var entityVars = new EntityVariables();
        Assert.False(entityVars.HasFactionReputation());
        
        var dummyFactionRep = new FactionReputation();
        entityVars.FactionReputation = dummyFactionRep;
        Assert.True(entityVars.HasFactionReputation());
        Assert.Equal(entityVars.FactionReputation, dummyFactionRep);
    }

    [Fact]
    public void should_use_same_faction_reputation_instance_on_subsequent_access()
    {
        var entityVars = new EntityVariables();
        var rep = entityVars.FactionReputation;
        rep[1] = 50;
        Assert.Equal(50, entityVars.FactionReputation[1]);
    }

    [Fact]
    public void should_correctly_handle_trigger_state_on_entity()
    {
        var entityVars = new EntityVariables();
        Assert.False(entityVars.HasTriggerState());
        
        var dummyQuestState = new TriggerState();
        entityVars.TriggerState = dummyQuestState;
        Assert.True(entityVars.HasTriggerState());
        Assert.Equal(entityVars.TriggerState, dummyQuestState);
    }

    [Fact]
    public void should_use_same_trigger_state_instance_on_subsequent_access()
    {
        var entityVars = new EntityVariables();
        var state = entityVars.TriggerState;
        state[1] = true;
        Assert.True(entityVars.TriggerState[1]);
    }

    [Fact]
    public void should_correctly_handle_active_quests_on_entity()
    {
        var entityVars = new EntityVariables();
        Assert.False(entityVars.HasActiveQuests());

        var dummyQuests = new List<QuestData> { new QuestData { TemplateId = 1 } };
        entityVars.ActiveQuests = dummyQuests;
        Assert.True(entityVars.HasActiveQuests());
        Assert.Equal(entityVars.ActiveQuests, dummyQuests);
    }

    [Fact]
    public void should_use_same_active_quests_instance_on_subsequent_access()
    {
        var entityVars = new EntityVariables();
        var quests = entityVars.ActiveQuests;
        quests.Add(new QuestData { TemplateId = 42 });
        Assert.Single(entityVars.ActiveQuests);
        Assert.Equal(42, entityVars.ActiveQuests[0].TemplateId);
    }

    [Fact]
    public void should_find_quest_data_by_template_id()
    {
        var entityVars = new EntityVariables();
        var quest1 = new QuestData { TemplateId = 10 };
        var quest2 = new QuestData { TemplateId = 20 };
        entityVars.ActiveQuests = new List<QuestData> { quest1, quest2 };

        var found = entityVars.FindQuestData(20);
        Assert.Equal(quest2, found);
    }

    [Fact]
    public void find_quest_data_should_return_null_when_not_found()
    {
        var entityVars = new EntityVariables();
        entityVars.ActiveQuests = new List<QuestData> { new QuestData { TemplateId = 10 } };

        var found = entityVars.FindQuestData(99);
        Assert.Null(found);
    }

    [Fact]
    public void find_quest_data_should_return_null_when_no_active_quests()
    {
        var entityVars = new EntityVariables();
        var found = entityVars.FindQuestData(10);
        Assert.Null(found);
    }
}
