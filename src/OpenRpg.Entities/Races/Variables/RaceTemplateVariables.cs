using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Races.Variables
{
    public class RaceTemplateVariables : ObjectVariables
    {
        public RaceTemplateVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.RaceTemplateVariables, internalVariables)
        {
        }
    }
}