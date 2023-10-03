using OpenRpg.Core.Variables.Entity;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventory;
using OpenRpg.Items.TradeSkills.Extensions;
using OpenRpg.Items.TradeSkills.State;
using Xunit;

namespace OpenRpg.UnitTests.TradeSkills;

public class TradeSkillsEntityVariableExtensionTests
{
    [Fact]
    public void should_correctly_handle_trade_skill_state_on_entity()
    {
        var entityVars = new DefaultEntityVariables();
        Assert.False(entityVars.HasTradeSkillState());
        
        var tradeSkillState = new DefaultTradeSkillState();
        entityVars.TradeSkillState(tradeSkillState);
        Assert.True(entityVars.HasTradeSkillState());
        Assert.Equal(entityVars.TradeSkillState(), tradeSkillState);
    }
}