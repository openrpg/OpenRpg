using System.Collections.Generic;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Variables;

namespace OpenRpg.Combat.Variables
{
    public class DefaultActiveEffectsVariables : DefaultVariables<object>, IActiveEffectsVariables
    {
        public DefaultActiveEffectsVariables(IDictionary<int, object> internalVariables = null) : base(CombatVariableTypes.ActiveEffectsVariable, internalVariables)
        {
        }
    }
}