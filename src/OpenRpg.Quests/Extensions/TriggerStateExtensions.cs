using System.Collections.Generic;
using OpenRpg.Quests.State;

namespace OpenRpg.Quests.Extensions;

public static class TriggerStateExtensions
{
    public static bool HasTriggered(this ITriggerState triggerState, int triggerId)
    { return triggerState.GetValueOrDefault(triggerId, false); }
    
    public static bool SetTrigger(this ITriggerState triggerState, int triggerId, bool activeState)
    { return triggerState[triggerId] = activeState; }
}