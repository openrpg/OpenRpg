using OpenRpg.Core.Variables.Entity;
using OpenRpg.Quests;
using OpenRpg.Quests.Extensions;
using OpenRpg.Quests.Factions;
using Xunit;

namespace OpenRpg.UnitTests.Quest;

public class QuestEntityVariableExtensionTests
{
    [Fact]
    public void should_correctly_handle_faction_reputation_on_entity()
    {
        var entityVars = new DefaultEntityVariables();
        Assert.False(entityVars.HasFactionReputation());
        
        var dummyFactionRep = new DefaultFactionReputation();
        entityVars.FactionReputation(dummyFactionRep);
        Assert.True(entityVars.HasFactionReputation());
        Assert.Equal(entityVars.FactionReputation(), dummyFactionRep);
    }
    
    [Fact]
    public void should_correctly_handle_quest_state_on_entity()
    {
        var entityVars = new DefaultEntityVariables();
        Assert.False(entityVars.HasQuestState());
        
        var dummyQuestState = new DefaultQuestState();
        entityVars.QuestState(dummyQuestState);
        Assert.True(entityVars.HasQuestState());
        Assert.Equal(entityVars.QuestState(), dummyQuestState);
    }
}