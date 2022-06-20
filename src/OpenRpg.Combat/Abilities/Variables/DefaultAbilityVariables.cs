using System.Collections.Generic;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Variables;

namespace OpenRpg.Combat.Abilities.Variables
{
    public class DefaultAbilityVariables : DefaultVariables<object>, IAbilityVariables
    {
        public DefaultAbilityVariables(IDictionary<int, object> internalVariables = null) : base(CombatVariableTypes.AbilityVariable, internalVariables)
        {
        }
    }
}