using System;
using OpenRpg.Genres.Persistence.Items;
using OpenRpg.Genres.Persistence.Items.Equipment;
using OpenRpg.Genres.Scifi.Equipment.CharacterSlots;
using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Items.Equipment;

namespace OpenRpg.Genres.Scifi.Persistence.Items.Equipment
{
    public class SciFiCharacterEquipmentMapper : EquipmentMapper, ISciFiCharacterEquipmentMapper
    {
        public IItemMapper ItemMapper { get; }

        public SciFiCharacterEquipmentMapper(IItemMapper itemMapper)
        { ItemMapper = itemMapper; }
        
        public override IEquipmentSlot GetSlotFor(int slotType, ItemData slottedItemData)
        {
            var item = ItemMapper.Map(slottedItemData);
            if(slotType == ScifiEntityEquipmentSlotTypes.ArmourSlot) { return new ArmourSlot(item); }
            if(slotType == ScifiEntityEquipmentSlotTypes.WeaponSlot) { return new WeaponSlot(item); }
            
            throw new Exception($"Cannot map slot type [{slotType}] as there is no known handler for it");
        }
    }
}