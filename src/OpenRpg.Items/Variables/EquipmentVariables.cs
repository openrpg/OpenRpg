using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Variables
{
    public class EquipmentVariables : ObjectVariables
    {
        public EquipmentVariables(IDictionary<int, object> internalVariables = null) : base(ItemCoreVariableTypes.EquipmentVariables, internalVariables)
        {
        }
    }
}