using OpenRpg.Core.Entity.Variables;
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
        entityVars.FactionReputation(dummyFactionRep);
        Assert.True(entityVars.HasFactionReputation());
        Assert.Equal(entityVars.FactionReputation(), dummyFactionRep);
    }
    
    [Fact]
    public void should_correctly_handle_quest_state_on_entity()
    {
        var entityVars = new EntityVariables();
        Assert.False(entityVars.HasQuestState());
        
        var dummyQuestState = new QuestState();
        entityVars.QuestState(dummyQuestState);
        Assert.True(entityVars.HasQuestState());
        Assert.Equal(entityVars.QuestState(), dummyQuestState);
    }
    
    [Fact]
    public void should_correctly_handle_trigger_state_on_entity()
    {
        var entityVars = new EntityVariables();
        Assert.False(entityVars.HasTriggerState());
        
        var dummyQuestState = new TriggerState();
        entityVars.TriggerState(dummyQuestState);
        Assert.True(entityVars.HasTriggerState());
        Assert.Equal(entityVars.TriggerState(), dummyQuestState);
    }
}