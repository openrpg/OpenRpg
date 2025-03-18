using System.Collections.Generic;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Races.Variables
{
    public class RaceTemplateVariables : ObjectVariables, ITemplateVariables
    {
        public RaceTemplateVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.RaceTemplateVariables, internalVariables)
        {
        }
    }
}