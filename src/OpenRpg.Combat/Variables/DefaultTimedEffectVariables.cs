using System.Collections.Generic;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Variables;

namespace OpenRpg.Combat.Variables
{
    public class DefaultTimedEffectVariables : DefaultVariables<object>, ITimedEffectVariables
    {
        public DefaultTimedEffectVariables(IDictionary<int, object> internalVariables = null) : base(CombatVariableTypes.TimedEffectVariable, internalVariables)
        {
        }
    }
}