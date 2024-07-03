using OpenRpg.Combat.Effects;
using OpenRpg.Combat.Extensions;
using OpenRpg.Core.Entity.Variables;
using Xunit;

namespace OpenRpg.UnitTests.Combat;

public class CombatEntityVariableExtensionTests
{
    [Fact]
    public void should_correctly_handle_activeeffects_on_entity()
    {
        var entityVars = new EntityVariables();
        Assert.False(entityVars.HasActiveEffects());
        
        var dummyActiveEffects = new DefaultActiveEffects();
        entityVars.ActiveEffects(dummyActiveEffects);
        Assert.True(entityVars.HasActiveEffects());
        Assert.Equal(entityVars.ActiveEffects(), dummyActiveEffects);
    }
}