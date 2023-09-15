using System.Collections.Generic;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Combat.Variables
{
    public class DefaultActiveEffectsVariables : ObjectVariables, IActiveEffectsVariables
    {
        public DefaultActiveEffectsVariables(IDictionary<int, object> internalVariables = null) : base(CombatVariableTypes.ActiveEffectsVariable, internalVariables)
        {
        }
    }
}