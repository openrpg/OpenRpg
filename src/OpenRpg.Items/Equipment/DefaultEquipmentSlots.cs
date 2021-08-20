using System.Collections.Generic;
using OpenRpg.Core.Variables;

namespace OpenRpg.Items.Equipment
{
    public class DefaultEquipmentSlots : DefaultVariables<IEquipmentSlot<IItem>>, IEquipmentSlots
    {
        public DefaultEquipmentSlots(IDictionary<int, IEquipmentSlot<IItem>> internalVariables = null) : base(internalVariables)
        {
        }
    }
}