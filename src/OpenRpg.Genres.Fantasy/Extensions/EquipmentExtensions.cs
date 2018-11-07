using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Equipment;
using OpenRpg.Items;
using OpenRpg.Items.Equipment;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class EquipmentExtensions
    {
        public static IEnumerable<Effect> GetItemEffects(this IItem item)
        {
            if(!item.Modifications.Any())
            { return item.ItemTemplate.Effects; }
         
            return item.ItemTemplate.Effects.Union(item.Modifications.SelectMany(x => x.Effects));
        }

        private static void ProcessEquipmentSlot(IEquipmentSlot<IItem> equipmentSlot, List<Effect> effectList)
        {
            if(equipmentSlot.SlottedItem == null) { return; }
            var effects = GetItemEffects(equipmentSlot.SlottedItem);
            effectList.AddRange(effects);
        }
        
        public static ICollection<Effect> GetEquipmentEffects(this IEquipment equipment)
        {
            var equipmentEffects = new List<Effect>();

            ProcessEquipmentSlot(equipment.NeckSlot, equipmentEffects);
            ProcessEquipmentSlot(equipment.OffHandSlot, equipmentEffects);
            ProcessEquipmentSlot(equipment.LowerBodySlot, equipmentEffects);
            ProcessEquipmentSlot(equipment.UpperBodySlot, equipmentEffects);
            ProcessEquipmentSlot(equipment.BackSlot, equipmentEffects);
            ProcessEquipmentSlot(equipment.FootSlot, equipmentEffects);
            ProcessEquipmentSlot(equipment.HeadSlot, equipmentEffects);
            ProcessEquipmentSlot(equipment.Ring1Slot, equipmentEffects);
            ProcessEquipmentSlot(equipment.Ring2Slot, equipmentEffects);
            ProcessEquipmentSlot(equipment.WristSlot, equipmentEffects);
            ProcessEquipmentSlot(equipment.MainHandSlot, equipmentEffects);

            return equipmentEffects;
        }
    }
}