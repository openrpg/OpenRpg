using System.Collections.Generic;
using OpenRpg.Core.Variables;

namespace OpenRpg.Core.Races
{
    public class DefaultRaceTemplateVariables : DefaultVariables<object>, IRaceTemplateVariables
    {
        public DefaultRaceTemplateVariables(IDictionary<int, object> internalVariables = null) : base(internalVariables)
        {
        }
    }
}