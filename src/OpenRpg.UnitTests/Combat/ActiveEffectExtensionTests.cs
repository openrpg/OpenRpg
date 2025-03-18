using OpenRpg.Combat.Effects;
using OpenRpg.Combat.Extensions;
using Xunit;

namespace OpenRpg.UnitTests.Combat;

public class ActiveEffectExtensionsTests
{
    [Theory]
    [InlineData(0, true)]
    [InlineData(1, false)]
    [InlineData(1000, false)]
    [InlineData(-1, false)]
    public void should_correctly_indicate_if_effect_is_passive(int effectFrequency, bool shouldBePassive)
    {
        var dummyTimedEffect = new TimedStaticEffect() { Frequency = effectFrequency };
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect);
        
        Assert.Equal(shouldBePassive, dummyActiveEffect.IsPassiveEffect());
    }
    
    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(2, 1, 2)]
    [InlineData(2, 2, 4)]
    [InlineData(10, 5, 50)]
    public void should_correctly_get_stacked_potency(int potency, int stacks, int expectedPotency)
    {
        var dummyTimedEffect = new TimedStaticEffect() { Potency = potency, MaxStack = stacks };
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect) { Stacks = stacks };
        var actualPotency = dummyActiveEffect.GetStackedPotency();
        Assert.Equal(expectedPotency, actualPotency);
    }
    
    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(2, 1, 0)]
    [InlineData(1, 2, 2)]
    [InlineData(1.5, 4, 2)]
    public void should_correctly_get_total_ticks(float frequency, float activeTime, int expectedTicks)
    {
        var dummyTimedEffect = new TimedStaticEffect() { Frequency = frequency};
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect) { ActiveTime = activeTime };
        var actualTicks = dummyActiveEffect.TicksSoFar();
        Assert.Equal(expectedTicks, actualTicks);
    }
}