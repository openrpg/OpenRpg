using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Effects;
using OpenRpg.Items.Equipment;

namespace OpenRpg.Items.Extensions
{
    public static class IEquipmentExtensions
    {
        public static bool HasItemEquipped(this IEquipmentSlot slot)
        { return slot.SlottedItem != null; }
        
        public static IEnumerable<Effect> GetEffects(this IEquipment equipment)
        {
            return equipment.Slots.Values
                .Where(x => x.HasItemEquipped())
                .SelectMany(x => x.SlottedItem.GetItemEffects());
        }
    }
}