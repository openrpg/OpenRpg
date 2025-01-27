using Moq;
using OpenRpg.Core.Utils;
using OpenRpg.Genres.Fantasy.Builders;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items.Extensions;
using Xunit;

namespace OpenRpg.UnitTests.Genres.Fantasy
{
    public class FantasyCharacterBuilderTests
    {
        [Fact]
        public void should_correctly_setup_slots_for_mapping_when_has_equipment()
        {
            var mockRandomizer = new Mock<IRandomizer>();
            var characterBuilder = new FantasyCharacterBuilder(mockRandomizer.Object);

            var character = characterBuilder.CreateNew().Build();
            
            Assert.NotNull(character);
            Assert.NotNull(character.Variables);

            var equipment = character.Variables.Equipment();
            Assert.NotNull(equipment);

            var slots = equipment.Slots;
            Assert.Contains(FantasyEquipmentSlotTypes.BackSlot, slots);
            Assert.Contains(FantasyEquipmentSlotTypes.HeadSlot, slots);
            Assert.Contains(FantasyEquipmentSlotTypes.UpperBodySlot, slots);
            Assert.Contains(FantasyEquipmentSlotTypes.LowerBodySlot, slots);
            Assert.Contains(FantasyEquipmentSlotTypes.FootSlot, slots);
            Assert.Contains(FantasyEquipmentSlotTypes.WristSlot, slots);
            Assert.Contains(FantasyEquipmentSlotTypes.MainHandSlot, slots);
            Assert.Contains(FantasyEquipmentSlotTypes.OffHandSlot , slots);
            Assert.Contains(FantasyEquipmentSlotTypes.NeckSlot, slots);
            Assert.Contains(FantasyEquipmentSlotTypes.Ring1Slot, slots);
            Assert.Contains(FantasyEquipmentSlotTypes.Ring2Slot, slots);
        }
    }
}