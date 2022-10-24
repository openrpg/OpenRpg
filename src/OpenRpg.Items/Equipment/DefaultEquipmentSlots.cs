using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Equipment
{
    public class DefaultEquipmentSlots : DefaultVariables<IEquipmentSlot>, IEquipmentSlots
    {
        public DefaultEquipmentSlots(IDictionary<int, IEquipmentSlot> internalVariables = null) : base(ItemVariableTypes.EquipmentSlotVariables, internalVariables)
        {
        }
    }
}