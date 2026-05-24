using OpenRpg.Combat.Effects;
using OpenRpg.Combat.Extensions;
using OpenRpg.Entities.Entity.Variables;
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
        entityVars.ActiveEffects = dummyActiveEffects;
        Assert.True(entityVars.HasActiveEffects());
        Assert.Equal(entityVars.ActiveEffects, dummyActiveEffects);
    }

    [Fact]
    public void should_use_same_active_effects_instance_on_subsequent_access()
    {
        var entityVars = new EntityVariables();
        var effects = entityVars.ActiveEffects;
        effects.AddEffect(new TimedStaticEffect { Id = 1, Duration = 10 });
        Assert.True(entityVars.ActiveEffects.HasEffect(1));
    }
}