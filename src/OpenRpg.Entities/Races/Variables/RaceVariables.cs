using System.Collections.Generic;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Races.Variables
{
    public class RaceVariables : ObjectVariables, ITemplateDataVariables
    {
        public RaceVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.RaceVariables, internalVariables)
        {
        }
    }
}