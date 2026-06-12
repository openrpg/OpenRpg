using OpenRpg.Entities.Entity.Variables;
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
    public void should_correctly_handle_quest_state_on_entity()
    {
        var entityVars = new EntityVariables();
        Assert.False(entityVars.HasQuestState());
        
        var dummyQuestState = new QuestState();
        entityVars.QuestState = dummyQuestState;
        Assert.True(entityVars.HasQuestState());
        Assert.Equal(entityVars.QuestState, dummyQuestState);
    }

    [Fact]
    public void should_use_same_quest_state_instance_on_subsequent_access()
    {
        var entityVars = new EntityVariables();
        var state = entityVars.QuestState;
        state[42] = 1;
        Assert.Equal(1, entityVars.QuestState[42]);
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
    public void should_correctly_handle_objective_state_on_entity()
    {
        var entityVars = new EntityVariables();
        Assert.False(entityVars.HasObjectiveState());

        var dummyObjectiveState = new ObjectiveState();
        entityVars.ObjectiveState = dummyObjectiveState;
        Assert.True(entityVars.HasObjectiveState());
        Assert.Equal(entityVars.ObjectiveState, dummyObjectiveState);
    }

    [Fact]
    public void should_use_same_objective_state_instance_on_subsequent_access()
    {
        var entityVars = new EntityVariables();
        var state = entityVars.ObjectiveState;
        state.SetObjectiveProgress(10, 0, 5);
        Assert.Equal(5, entityVars.ObjectiveState.GetObjectiveProgress(10, 0));
    }
}