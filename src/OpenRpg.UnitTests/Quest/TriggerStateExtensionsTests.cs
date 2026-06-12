using OpenRpg.Quests.Extensions;
using OpenRpg.Quests.State;
using Xunit;

namespace OpenRpg.UnitTests.Quest;

public class TriggerStateExtensionsTests
{
    [Fact]
    public void has_triggered_should_return_false_when_trigger_not_set()
    {
        var triggerState = new TriggerState();
        Assert.False(triggerState.HasTriggered(1));
    }

    [Fact]
    public void set_trigger_should_store_and_return_state()
    {
        var triggerState = new TriggerState();
        var result = triggerState.SetTrigger(1, true);
        Assert.True(result);
        Assert.True(triggerState.HasTriggered(1));
    }

    [Fact]
    public void set_trigger_false_should_store_false()
    {
        var triggerState = new TriggerState();
        triggerState.SetTrigger(1, true);
        var result = triggerState.SetTrigger(1, false);
        Assert.False(result);
        Assert.False(triggerState.HasTriggered(1));
    }

    [Fact]
    public void has_triggered_should_return_set_value()
    {
        var triggerState = new TriggerState();
        triggerState.SetTrigger(1, true);
        Assert.True(triggerState.HasTriggered(1));
    }

    [Fact]
    public void should_track_multiple_triggers_independently()
    {
        var triggerState = new TriggerState();
        triggerState.SetTrigger(1, true);
        triggerState.SetTrigger(2, false);

        Assert.True(triggerState.HasTriggered(1));
        Assert.False(triggerState.HasTriggered(2));
        Assert.False(triggerState.HasTriggered(3));
    }

    [Fact]
    public void set_trigger_should_overwrite_existing_value()
    {
        var triggerState = new TriggerState();
        triggerState.SetTrigger(1, true);
        triggerState.SetTrigger(1, false);
        Assert.False(triggerState.HasTriggered(1));
    }

    [Fact]
    public void has_triggered_should_return_false_for_unset_trigger()
    {
        var triggerState = new TriggerState();
        Assert.False(triggerState.HasTriggered(999));
    }
}
