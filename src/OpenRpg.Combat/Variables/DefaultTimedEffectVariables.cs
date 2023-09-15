using System.Collections.Generic;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Combat.Variables
{
    public class DefaultTimedEffectVariables : ObjectVariables, ITimedEffectVariables
    {
        public DefaultTimedEffectVariables(IDictionary<int, object> internalVariables = null) : base(CombatVariableTypes.TimedEffectVariable, internalVariables)
        {
        }
    }
}