using System.Collections.Generic;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Genres.Scifi.Types;

namespace OpenRpg.Genres.Scifi.Variables
{
    public class ShipVariables : ObjectVariables, ITemplateDataVariables
    {
        public ShipVariables(IDictionary<int, object> internalVariables = null) : base(ScifiVariableTypes.ShipVariables, internalVariables)
        {
        }
    }
}