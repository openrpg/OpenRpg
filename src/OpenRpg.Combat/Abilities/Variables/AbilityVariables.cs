using System.Collections.Generic;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Combat.Abilities.Variables
{
    public class AbilityVariables : ObjectVariables, ITemplateDataVariables
    {
        public AbilityVariables(IDictionary<int, object> internalVariables = null) : base(CombatVariableTypes.AbilityVariables, internalVariables)
        {
        }
    }
}