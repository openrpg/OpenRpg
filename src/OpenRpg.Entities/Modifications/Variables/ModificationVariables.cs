using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Modifications.Variables
{
    public class ModificationVariables : ObjectVariables
    {
        public ModificationVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.ModificationVariables, internalVariables)
        {
        }
    }
}