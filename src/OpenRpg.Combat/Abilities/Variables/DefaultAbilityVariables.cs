using System.Collections.Generic;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Combat.Abilities.Variables
{
    public class DefaultAbilityVariables : ObjectVariables, IAbilityVariables
    {
        public DefaultAbilityVariables(IDictionary<int, object> internalVariables = null) : base(CombatVariableTypes.AbilityVariable, internalVariables)
        {
        }
    }
}