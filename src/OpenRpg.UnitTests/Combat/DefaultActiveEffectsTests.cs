using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Effects;
using Xunit;

namespace OpenRpg.UnitTests.Combat;

public class DefaultActiveEffectsTests
{
    [Fact]
    public void should_return_true_and_fire_event_and_add_effect_when_effect_not_added()
    {
        var dummyTimedEffect = new TimedEffect() { MaxStack = 2 };
        
        var activeEffects = new DefaultActiveEffects();
        
        var timesAdded = 0;
        activeEffects.EffectAdded += (_, effect) =>
        {
            Assert.Same(dummyTimedEffect, effect.Effect);
            timesAdded++;
        };
        
        var hasAdded = activeEffects.AddEffect(dummyTimedEffect);
        
        Assert.True(hasAdded);
        Assert.Equal(1, timesAdded);
        Assert.True(activeEffects.InternalActiveEffects.ContainsKey(dummyTimedEffect.Id));
        
        var activeEffect = activeEffects.InternalActiveEffects[dummyTimedEffect.Id];
        Assert.Equal(1, activeEffect.Stacks);
    }
    
    [Fact]
    public void should_return_true_and_add_effect_stack_without_add_event_and_reset_active_time_when_stacking_enabled_and_not_maxed()
    {
        var dummyTimedEffect = new TimedEffect() { MaxStack = 2 };
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect) { ActiveTime = 2 };
        
        var activeEffects = new DefaultActiveEffects();
        activeEffects.InternalActiveEffects.Add(dummyTimedEffect.Id, dummyActiveEffect);
        
        var timesAdded = 0;
        activeEffects.EffectAdded += (_, effect) =>
        {
            timesAdded++;
        };
        
        var hasAdded = activeEffects.AddEffect(dummyTimedEffect);
        
        Assert.True(hasAdded);
        Assert.Equal(0, timesAdded);
        Assert.True(activeEffects.InternalActiveEffects.ContainsKey(dummyTimedEffect.Id));
        
        var activeEffect = activeEffects.InternalActiveEffects[dummyTimedEffect.Id];
        Assert.Same(dummyActiveEffect, activeEffect);
        Assert.Equal(2, activeEffect.Stacks);
        Assert.Equal(0, activeEffect.ActiveTime);
    }
    
    [Fact]
    public void should_return_true_and_reset_active_time_if_stacks_are_maxed()
    {
        var dummyTimedEffect = new TimedEffect() { MaxStack = 2 };
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect) { Stacks = 2, ActiveTime = 2 };
        
        var activeEffects = new DefaultActiveEffects();
        activeEffects.InternalActiveEffects.Add(dummyTimedEffect.Id, dummyActiveEffect);
        
        var hasAdded = activeEffects.AddEffect(dummyTimedEffect);
        
        Assert.True(hasAdded);
        Assert.True(activeEffects.InternalActiveEffects.ContainsKey(dummyTimedEffect.Id));
        
        var activeEffect = activeEffects.InternalActiveEffects[dummyTimedEffect.Id];
        Assert.Same(dummyActiveEffect, activeEffect);
        Assert.Equal(2, activeEffect.Stacks);
        Assert.Equal(0, activeEffect.ActiveTime);
    }
    
    [Fact]
    public void should_return_true_and_reset_active_time_and_not_increment_stacks_if_not_stackable()
    {
        var dummyTimedEffect = new TimedEffect();
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect) {ActiveTime = 2 };
        
        var activeEffects = new DefaultActiveEffects();
        activeEffects.InternalActiveEffects.Add(dummyTimedEffect.Id, dummyActiveEffect);
        
        var hasAdded = activeEffects.AddEffect(dummyTimedEffect);
        
        Assert.True(hasAdded);
        Assert.True(activeEffects.InternalActiveEffects.ContainsKey(dummyTimedEffect.Id));
        
        var activeEffect = activeEffects.InternalActiveEffects[dummyTimedEffect.Id];
        Assert.Same(dummyActiveEffect, activeEffect);
        Assert.Equal(1, activeEffect.Stacks);
        Assert.Equal(0, activeEffect.ActiveTime);
    }
        
    [Fact]
    public void should_raise_trigger_event_when_frequency_met_and_still_active_during_update()
    {
        var dummyTimedEffect = new TimedEffect() { Duration = 100, Frequency = 1 };
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect);
        var activeEffects = new DefaultActiveEffects();
        activeEffects.InternalActiveEffects.Add(dummyTimedEffect.Id, dummyActiveEffect);

        var timesTriggered = 0;
        activeEffects.EffectTriggered += (_, effect) =>
        {
            Assert.Equal(dummyActiveEffect, effect);
            timesTriggered++;
        };
        activeEffects.UpdateEffects(1);
        
        Assert.Equal(1, timesTriggered);
    }
    
    [Fact]
    public void should_raise_trigger_event_multiple_events_when_frequency_met_multiple_times_and_still_active_during_update()
    {
        var dummyTimedEffect = new TimedEffect() { Duration = 100, Frequency = 1 };
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect);
        var activeEffects = new DefaultActiveEffects();
        activeEffects.InternalActiveEffects.Add(dummyTimedEffect.Id, dummyActiveEffect);

        var timesTriggered = 0;
        activeEffects.EffectTriggered += (_, effect) =>
        {
            Assert.Equal(dummyActiveEffect, effect);
            timesTriggered++;
        };
        activeEffects.UpdateEffects(2);
        
        Assert.Equal(2, timesTriggered);
    }
    
    [Fact]
    public void should_not_raise_trigger_event_when_expired_during_update()
    {
        var dummyTimedEffect = new TimedEffect() { Duration = 5, Frequency = 1 };
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect) { ActiveTime = 5};
        var activeEffects = new DefaultActiveEffects();
        activeEffects.InternalActiveEffects.Add(dummyTimedEffect.Id, dummyActiveEffect);

        var timesTriggered = 0;
        activeEffects.EffectTriggered += (_, effect) =>
        {
            Assert.Equal(dummyActiveEffect, effect);
            timesTriggered++;
        };
        activeEffects.UpdateEffects(2);
        
        Assert.Equal(0, timesTriggered);
    }
    
    [Fact]
    public void should_only_raise_allowable_triggers_over_larger_than_duration_period_during_update()
    {
        var dummyTimedEffect = new TimedEffect() { Duration = 5, Frequency = 1 };
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect);
        var activeEffects = new DefaultActiveEffects();
        activeEffects.InternalActiveEffects.Add(dummyTimedEffect.Id, dummyActiveEffect);

        var timesTriggered = 0;
        activeEffects.EffectTriggered += (_, effect) =>
        {
            Assert.Equal(dummyActiveEffect, effect);
            timesTriggered++;
        };
        activeEffects.UpdateEffects(100);
        
        Assert.Equal(5, timesTriggered);
    }
    
    [Fact]
    public void should_raise_event_when_expired_during_update()
    {
        var dummyTimedEffect = new TimedEffect() { Duration = 5, Frequency = 1 };
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect) { ActiveTime = 5};
        var activeEffects = new DefaultActiveEffects();
        activeEffects.InternalActiveEffects.Add(dummyTimedEffect.Id, dummyActiveEffect);

        var timesExpired = 0;
        activeEffects.EffectExpired += (_, effect) =>
        {
            Assert.Equal(dummyActiveEffect, effect);
            timesExpired++;
        };
        activeEffects.UpdateEffects(2);
        
        Assert.Equal(1, timesExpired);
    }
    
    [Fact]
    public void should_raise_expiry_once_even_with_multiple_updates()
    {
        var dummyTimedEffect = new TimedEffect() { Duration = 2, Frequency = 1 };
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect) { ActiveTime = 1 };
        var activeEffects = new DefaultActiveEffects();
        activeEffects.InternalActiveEffects.Add(dummyTimedEffect.Id, dummyActiveEffect);

        var timesExpired = 0;
        activeEffects.EffectExpired += (_, effect) =>
        {
            Assert.Equal(dummyActiveEffect, effect);
            timesExpired++;
        };
        activeEffects.UpdateEffects(1);
        activeEffects.UpdateEffects(1);
        activeEffects.UpdateEffects(1);
        
        Assert.Equal(1, timesExpired);
    }
    
    [Fact]
    public void should_remove_expired_during_update()
    {
        var dummyTimedEffect1 = new TimedEffect() { Id = 1, Duration = 2};
        var dummyActiveEffect1 = new ActiveEffect(dummyTimedEffect1) { ActiveTime = 2 };

        var dummyTimedEffect2 = new TimedEffect() { Id = 2, Duration = 2};
        var dummyActiveEffect2 = new ActiveEffect(dummyTimedEffect2);
        
        var activeEffects = new DefaultActiveEffects();
        activeEffects.InternalActiveEffects.Add(dummyTimedEffect1.Id, dummyActiveEffect1);
        activeEffects.InternalActiveEffects.Add(dummyTimedEffect2.Id, dummyActiveEffect2);

        var timesExpired = 0;
        activeEffects.EffectExpired += (_, effect) =>
        {
            Assert.Equal(dummyActiveEffect1, effect);
            timesExpired++;
        };
        activeEffects.UpdateEffects(1);
        
        Assert.Equal(1, timesExpired);
        Assert.Equal(1, dummyActiveEffect2.ActiveTime);
        Assert.Equal(3, dummyActiveEffect1.ActiveTime);
        Assert.Equal(1, activeEffects.InternalActiveEffects.Count);
    }
    
    [Fact]
    public void should_remove_effect_and_raise_event_if_exists()
    {
        var dummyTimedEffect1 = new TimedEffect() { Id = 1, Duration = 2};
        var dummyActiveEffect1 = new ActiveEffect(dummyTimedEffect1);

        var dummyTimedEffect2 = new TimedEffect() { Id = 2, Duration = 2};
        var dummyActiveEffect2 = new ActiveEffect(dummyTimedEffect2);
        
        var activeEffects = new DefaultActiveEffects();
        activeEffects.InternalActiveEffects.Add(dummyTimedEffect1.Id, dummyActiveEffect1);
        activeEffects.InternalActiveEffects.Add(dummyTimedEffect2.Id, dummyActiveEffect2);

        var timesExpired = 0;
        activeEffects.EffectExpired += (_, effect) =>
        {
            Assert.Equal(dummyActiveEffect1, effect);
            timesExpired++;
        };
        var hasRemoved = activeEffects.RemoveEffect(dummyTimedEffect1.Id);
        
        Assert.True(hasRemoved);
        Assert.Equal(1, timesExpired);
        Assert.Equal(1, activeEffects.InternalActiveEffects.Count);
        Assert.False(activeEffects.InternalActiveEffects.ContainsValue(dummyActiveEffect1));
    }
    
    [Fact]
    public void should_not_remove_effect_or_raise_event_if_doesnt_exists()
    {
        var dummyTimedEffect1 = new TimedEffect() { Id = 1, Duration = 2};
        
        var activeEffects = new DefaultActiveEffects();

        var timesExpired = 0;
        activeEffects.EffectExpired += (_, effect) =>
        {
            timesExpired++;
        };
        var hasRemoved = activeEffects.RemoveEffect(dummyTimedEffect1.Id);
        
        Assert.False(hasRemoved);
        Assert.Equal(0, timesExpired);
        Assert.Empty(activeEffects.InternalActiveEffects);
    }
    
    [Fact]
    public void should_return_true_when_effect_exists()
    {
        var dummyTimedEffect = new TimedEffect() { Id = 1 };
        var dummyActiveEffect = new ActiveEffect(dummyTimedEffect);
        
        var activeEffects = new DefaultActiveEffects();
        activeEffects.InternalActiveEffects.Add(dummyTimedEffect.Id, dummyActiveEffect);
        
        var exists = activeEffects.HasEffect(dummyTimedEffect.Id);
        
        Assert.True(exists);
    }
    
    [Fact]
    public void should_return_false_when_effect_doesnt_exist()
    {
        var dummyTimedEffect = new TimedEffect() { Id = 1 };
        var activeEffects = new DefaultActiveEffects();
        var exists = activeEffects.HasEffect(dummyTimedEffect.Id);
        
        Assert.False(exists);
    }
    
    [Fact]
    public void should_expose_only_passive_effects_for_has_effects_implementation()
    {
        var nonPassiveEffect = new TimedEffect() { Id = 1, Frequency = 1, EffectType = 1};
        var dummyActiveEffect1 = new ActiveEffect(nonPassiveEffect);

        var passiveEffect = new TimedEffect() { Id = 2, EffectType = 2 };
        var dummyActiveEffect2 = new ActiveEffect(passiveEffect);
        
        var activeEffects = new DefaultActiveEffects();
        activeEffects.InternalActiveEffects.Add(nonPassiveEffect.Id, dummyActiveEffect1);
        activeEffects.InternalActiveEffects.Add(passiveEffect.Id, dummyActiveEffect2);

        var passiveEffects = activeEffects.Effects.ToArray();
        
        Assert.Single(passiveEffects);
        Assert.Contains(passiveEffects, x => x.EffectType == passiveEffect.EffectType);
    }
    
    [Fact]
    public void should_calculate_stacked_potency_for_passive_effects()
    {
        var passiveEffect = new TimedEffect() { Id = 2, EffectType = 2, Potency = 3 };
        var dummyActiveEffect = new ActiveEffect(passiveEffect) { Stacks = 2};
        
        var activeEffects = new DefaultActiveEffects();
        activeEffects.InternalActiveEffects.Add(passiveEffect.Id, dummyActiveEffect);

        var passiveEffects = activeEffects.Effects.ToArray();
        
        Assert.Single(passiveEffects);
        Assert.Equal(passiveEffect.EffectType, passiveEffects[0].EffectType);
        Assert.Equal(6, passiveEffects[0].Potency);
    }
}