using System;
using OpenRpg.Genres.Persistence.Items;
using OpenRpg.Genres.Persistence.Items.Equipment;
using OpenRpg.Genres.Scifi.Equipment.ShipSlots;
using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Items.Equipment;
using WeaponSlot = OpenRpg.Genres.Scifi.Equipment.CharacterSlots.WeaponSlot;

namespace OpenRpg.Genres.Scifi.Persistence.Items.Equipment
{
    public class SciFiShipEquipmentMapper : EquipmentMapper, ISciFiShipEquipmentMapper
    {
        public IItemMapper ItemMapper { get; }

        public SciFiShipEquipmentMapper(IItemMapper itemMapper)
        { ItemMapper = itemMapper; }
        
        public override IEquipmentSlot GetSlotFor(int slotType, ItemData slottedItemData)
        {
            var item = ItemMapper.Map(slottedItemData);
            if(slotType == ShipEquipmentSlotTypes.EngineSlot) { return new EngineSlot(item); }
            if(slotType == ShipEquipmentSlotTypes.MiscSlot1) { return new MiscSlot(item); }
            if(slotType == ShipEquipmentSlotTypes.MiscSlot2) { return new MiscSlot(item); }
            if(slotType == ShipEquipmentSlotTypes.MiscSlot3) { return new MiscSlot(item); }
            if(slotType == ShipEquipmentSlotTypes.MiscSlot4) { return new MiscSlot(item); }
            if(slotType == ShipEquipmentSlotTypes.ShieldSlot) { return new ShieldSlot(item); }
            if(slotType == ShipEquipmentSlotTypes.WeaponSlot1) { return new WeaponSlot(item); }
            if(slotType == ShipEquipmentSlotTypes.WeaponSlot2) { return new WeaponSlot(item); }
            if(slotType == ShipEquipmentSlotTypes.WeaponSlot3) { return new WeaponSlot(item); }
            if(slotType == ShipEquipmentSlotTypes.WeaponSlot4) { return new WeaponSlot(item); }
            if(slotType == ShipEquipmentSlotTypes.WeaponSlot5) { return new WeaponSlot(item); }
            if(slotType == ShipEquipmentSlotTypes.WeaponSlot6) { return new WeaponSlot(item); }
            if(slotType == ShipEquipmentSlotTypes.WingsSlot) { return new WingSlot(item); }
            if(slotType == ShipEquipmentSlotTypes.HullArmourSlot) { return new HullArmourSlot(item); }
            
            throw new Exception($"Cannot map slot type [{slotType}] as there is no known handler for it");
        }
    }
}