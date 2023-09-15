using System.Collections.Generic;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Races.Variables
{
    public class DefaultRaceTemplateVariables : ObjectVariables, IRaceTemplateVariables
    {
        public DefaultRaceTemplateVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.RaceTemplateVariables, internalVariables)
        {
        }
    }
}