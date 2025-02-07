using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Effects;
using OpenRpg.Items.Equippables;
using OpenRpg.Items.Equippables.Slots;
using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Extensions
{
    public static class EquipmentExtensions
    {
        public static bool HasItemEquipped(this EquipmentSlots slots, int slotType)
        {
            if(!slots.ContainsKey(slotType)) { return false; }
            return slots[slotType] != null;
        }
        
        public static bool HasItemEquipped(this Equipment equipment, int slotType)
        {
            if(!equipment.Slots.ContainsKey(slotType)) { return false; }
            return equipment.Slots[slotType] != null;
        }

        public static bool HasSlot(this Equipment equipment, int slotType)
        { return equipment.Slots.ContainsKey(slotType); }
        
        public static ItemData AttemptUnequipSlot(this Equipment equipment, int slotType)
        {
            if(!equipment.HasSlot(slotType)) { return null; }
            if(!equipment.HasItemEquipped(slotType)) { return null; }

            var returnedItem = equipment.Slots[slotType];
            equipment.Slots[slotType] = null;
            return returnedItem;
        }

        public static bool AttemptEquipSlot(this Equipment equipment, IEquipmentSlotValidator slotValidator, int slotType, ItemData itemData, ItemTemplate template)
        {
            if(!equipment.HasSlot(slotType)) { return false; }
            if(!equipment.HasItemEquipped(slotType)) { return false; }
            if(slotValidator.CanEquipItemType(slotType, template.ItemType)) { return false; }
            equipment.Slots[slotType] = itemData;
            return true;
        }
        
        public static IEnumerable<StaticEffect> GetEffects(this Equipment equipment, ITemplateAccessor templateAccessor)
        {
            return equipment.Slots.Values
                .Where(x => x != null)
                .SelectMany(x => x.GetItemEffects(templateAccessor.GetItemTemplate(x.TemplateId)));
        }
    }
}