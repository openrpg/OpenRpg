using System.Collections.Generic;
using OpenRpg.Core.Variables;

namespace OpenRpg.Combat.Abilities
{
    public class DefaultAbilityVariables : DefaultVariables<object>, IAbilityVariables
    {
        public DefaultAbilityVariables(IDictionary<int, object> internalVariables = null) : base(internalVariables)
        {
        }
    }
}