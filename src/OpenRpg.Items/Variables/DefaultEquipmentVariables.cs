using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Variables
{
    public class DefaultEquipmentVariables : ObjectVariables, IEquipmentVariables
    {
        public DefaultEquipmentVariables(IDictionary<int, object> internalVariables = null) : base(ItemCoreVariableTypes.EquipmentVariables, internalVariables)
        {
        }
    }
}