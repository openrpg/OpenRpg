using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Variables
{
    public class DefaultEquipmentVariables : DefaultVariables<object>, IEquipmentVariables
    {
        public DefaultEquipmentVariables(IDictionary<int, object> internalVariables = null) : base(ItemVariableTypes.EquipmentVariables, internalVariables)
        {
        }
    }
}