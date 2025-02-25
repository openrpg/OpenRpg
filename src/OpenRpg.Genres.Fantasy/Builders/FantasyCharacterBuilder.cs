using OpenRpg.Core.Utils;
using OpenRpg.Genres.Builders;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Genres.Populators.Entity;

namespace OpenRpg.Genres.Fantasy.Builders
{
    public class FantasyCharacterBuilder : CharacterBuilder
    {
        public FantasyCharacterBuilder(IRandomizer randomizer, ICharacterPopulator characterPopulator) : base(randomizer, characterPopulator)
        {
        }

        protected override void PreProcessCharacter()
        {
            _equipment.TryAdd(FantasyEquipmentSlotTypes.HeadSlot, null);
            _equipment.TryAdd(FantasyEquipmentSlotTypes.BackSlot, null);
            _equipment.TryAdd(FantasyEquipmentSlotTypes.UpperBodySlot, null);
            _equipment.TryAdd(FantasyEquipmentSlotTypes.LowerBodySlot, null);
            _equipment.TryAdd(FantasyEquipmentSlotTypes.FootSlot, null);
            _equipment.TryAdd(FantasyEquipmentSlotTypes.MainHandSlot, null);
            _equipment.TryAdd(FantasyEquipmentSlotTypes.OffHandSlot, null);
            _equipment.TryAdd(FantasyEquipmentSlotTypes.NeckSlot, null);
            _equipment.TryAdd(FantasyEquipmentSlotTypes.Ring1Slot, null);
            _equipment.TryAdd(FantasyEquipmentSlotTypes.Ring2Slot, null);
            _equipment.TryAdd(FantasyEquipmentSlotTypes.WristSlot, null);

            base.PreProcessCharacter();
        }
    }
}