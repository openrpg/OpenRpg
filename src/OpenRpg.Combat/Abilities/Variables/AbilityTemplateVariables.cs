using System.Collections.Generic;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Combat.Abilities.Variables
{
    public class AbilityTemplateVariables : ObjectVariables, ITemplateVariables
    {
        public AbilityTemplateVariables(IDictionary<int, object> internalVariables = null) : base(CombatVariableTypes.AbilityTemplateVariables, internalVariables)
        {
        }
    }
}