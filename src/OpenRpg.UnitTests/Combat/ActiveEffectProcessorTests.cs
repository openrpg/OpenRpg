using OpenRpg.Combat.Effects;
using OpenRpg.Combat.Processors.Effects;
using Xunit;

namespace OpenRpg.UnitTests.Combat;

public class ActiveEffectProcessorTests
{
    [Fact]
    public void should_raise_trigger_event_when_frequency_met_and_still_active()
    {
        var dummyTimedEffect = new TimedEffect() { Duration = 100, Frequency = 1 };
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect);
        var dummyActiveEffects = new[] { dummyActiveEffect };

        var timesTriggered = 0;
        var activeEffectProcessor = new ActiveEffectProcessor();
        activeEffectProcessor.EffectTriggered += (_, effect) =>
        {
            Assert.Equal(dummyActiveEffect, effect);
            timesTriggered++;
        };
        activeEffectProcessor.Process(dummyActiveEffects, 1);
        
        Assert.Equal(1, timesTriggered);
    }
    
    [Fact]
    public void should_raise_trigger_event_multiple_events_when_frequency_met_multiple_times_and_still_active()
    {
        var dummyTimedEffect = new TimedEffect() { Duration = 100, Frequency = 1 };
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect);
        var dummyActiveEffects = new[] { dummyActiveEffect };

        var timesTriggered = 0;
        var activeEffectProcessor = new ActiveEffectProcessor();
        activeEffectProcessor.EffectTriggered += (_, effect) =>
        {
            Assert.Equal(dummyActiveEffect, effect);
            timesTriggered++;
        };
        activeEffectProcessor.Process(dummyActiveEffects, 2);
        
        Assert.Equal(2, timesTriggered);
    }
    
    [Fact]
    public void should_not_raise_trigger_event_when_expired()
    {
        var dummyTimedEffect = new TimedEffect() { Duration = 5, Frequency = 1 };
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect) { ActiveTime = 5};
        var dummyActiveEffects = new[] { dummyActiveEffect };

        var timesTriggered = 0;
        var activeEffectProcessor = new ActiveEffectProcessor();
        activeEffectProcessor.EffectTriggered += (_, effect) =>
        {
            Assert.Equal(dummyActiveEffect, effect);
            timesTriggered++;
        };
        activeEffectProcessor.Process(dummyActiveEffects, 2);
        
        Assert.Equal(0, timesTriggered);
    }
    
    [Fact]
    public void should_raise_event_when_expired()
    {
        var dummyTimedEffect = new TimedEffect() { Duration = 5, Frequency = 1 };
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect) { ActiveTime = 5};
        var dummyActiveEffects = new[] { dummyActiveEffect };

        var timesExpired = 0;
        var activeEffectProcessor = new ActiveEffectProcessor();
        activeEffectProcessor.EffectExpired += (_, effect) =>
        {
            Assert.Equal(dummyActiveEffect, effect);
            timesExpired++;
        };
        activeEffectProcessor.Process(dummyActiveEffects, 2);
        
        Assert.Equal(1, timesExpired);
    }
    
    [Fact]
    public void should_raise_expiry_each_update_its_expired()
    {
        var dummyTimedEffect = new TimedEffect() { Duration = 2, Frequency = 1 };
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect) { ActiveTime = 1 };
        var dummyActiveEffects = new[] { dummyActiveEffect };

        var timesExpired = 0;
        var activeEffectProcessor = new ActiveEffectProcessor();
        activeEffectProcessor.EffectExpired += (_, effect) =>
        {
            Assert.Equal(dummyActiveEffect, effect);
            timesExpired++;
        };
        activeEffectProcessor.Process(dummyActiveEffects, 1);
        activeEffectProcessor.Process(dummyActiveEffects, 1);
        activeEffectProcessor.Process(dummyActiveEffects, 1);
        
        Assert.Equal(2, timesExpired);
    }
    
    [Fact]
    public void should_correctly_raise_triggers_before_expiring()
    {
        var dummyTimedEffect = new TimedEffect() { Duration = 2, Frequency = 1 };
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect);
        var dummyActiveEffects = new[] { dummyActiveEffect };

        var timesExpired = 0;
        var timesTriggered = 0;
        var activeEffectProcessor = new ActiveEffectProcessor();
        activeEffectProcessor.EffectExpired += (_, effect) =>
        {
            Assert.Equal(dummyActiveEffect, effect);
            timesExpired++;
        };
        activeEffectProcessor.EffectTriggered += (_, effect) =>
        {
            Assert.Equal(dummyActiveEffect, effect);
            timesTriggered++;
        };
        activeEffectProcessor.Process(dummyActiveEffects, 1);
        activeEffectProcessor.Process(dummyActiveEffects, 1);
        activeEffectProcessor.Process(dummyActiveEffects, 0);
        
        Assert.Equal(1, timesExpired);
        Assert.Equal(2, timesTriggered);
    }
}