using System.Collections.Generic;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Genres.Scifi.Types;

namespace OpenRpg.Genres.Scifi.Variables;

public class ShipTemplateVariables : ObjectVariables, ITemplateVariables
{
    public ShipTemplateVariables(IDictionary<int, object> internalVariables = null) : base(ScifiVariableTypes.ShipTemplateVariables, internalVariables)
    {
    }
}