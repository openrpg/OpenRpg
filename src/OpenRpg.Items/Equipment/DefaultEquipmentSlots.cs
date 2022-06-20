using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Equipment
{
    public class DefaultEquipmentSlots : DefaultVariables<IEquipmentSlot<IItem>>, IEquipmentSlots
    {
        public DefaultEquipmentSlots(IDictionary<int, IEquipmentSlot<IItem>> internalVariables = null) : base(ItemVariableTypes.EquipmentSlotVariables, internalVariables)
        {
        }
    }
}