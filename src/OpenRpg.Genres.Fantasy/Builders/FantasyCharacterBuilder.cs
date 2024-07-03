using OpenRpg.Core.Utils;
using OpenRpg.Genres.Builders;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Builders
{
    public class FantasyCharacterBuilder : CharacterBuilder
    {
        public FantasyCharacterBuilder(IRandomizer randomizer) : base(randomizer)
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

        protected override void PostProcessCharacter(Character character)
        {
            if (!character.State.ContainsKey(FantasyEntityStateVariableTypes.Health))
            { character.State.Health(character.Stats.MaxHealth()); }
            
            if (!character.State.ContainsKey(FantasyEntityStateVariableTypes.Magic))
            { character.State.Magic(character.Stats.MaxMagic()); }
            
            base.PostProcessCharacter(character);
        }
    }
}