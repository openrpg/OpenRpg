using System.Collections.Generic;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Races.Variables
{
    public class RaceTemplateVariables : ObjectVariables
    {
        public RaceTemplateVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.RaceTemplateVariables, internalVariables)
        {
        }
    }
}