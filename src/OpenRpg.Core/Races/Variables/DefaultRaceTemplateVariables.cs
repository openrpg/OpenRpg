using System.Collections.Generic;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables;

namespace OpenRpg.Core.Races.Variables
{
    public class DefaultRaceTemplateVariables : DefaultVariables<object>, IRaceTemplateVariables
    {
        public DefaultRaceTemplateVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.RaceTemplateVariables, internalVariables)
        {
        }
    }
}