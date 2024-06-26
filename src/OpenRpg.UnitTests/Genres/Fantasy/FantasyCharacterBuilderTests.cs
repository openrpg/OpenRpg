using Moq;
using OpenRpg.Core.Utils;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Fantasy.Builders;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Genres.Persistence.Characters;
using OpenRpg.Genres.Persistence.Items.Equipment;
using OpenRpg.Genres.Types;
using Xunit;

namespace OpenRpg.UnitTests.Genres.Fantasy
{
    public class FantasyCharacterBuilderTests
    {
        [Fact]
        public void should_correctly_setup_slots_for_mapping_when_has_equipment()
        {
            var mockRandomizer = new Mock<IRandomizer>();
            var mockCharacterMapper = new Mock<ICharacterMapper>();
            mockCharacterMapper
                .Setup(x => x.Map(It.IsAny<CharacterData>()))
                .Returns(new DefaultCharacter());
            
            var characterBuilder = new FantasyCharacterBuilder(mockCharacterMapper.Object, mockRandomizer.Object);

            characterBuilder.CreateNew().Build();
            
            mockCharacterMapper.Verify(x => x.Map(It.Is<CharacterData>(y => 
                y.Variables.ContainsKey(GenreEntityVariableTypes.Equipment) && 
                (y.Variables[GenreEntityVariableTypes.Equipment] as EquipmentData).Slots.ContainsKey(FantasyEquipmentSlotTypes.BackSlot) &&
                (y.Variables[GenreEntityVariableTypes.Equipment] as EquipmentData).Slots.ContainsKey(FantasyEquipmentSlotTypes.HeadSlot) &&
                (y.Variables[GenreEntityVariableTypes.Equipment] as EquipmentData).Slots.ContainsKey(FantasyEquipmentSlotTypes.UpperBodySlot) &&
                (y.Variables[GenreEntityVariableTypes.Equipment] as EquipmentData).Slots.ContainsKey(FantasyEquipmentSlotTypes.LowerBodySlot) &&
                (y.Variables[GenreEntityVariableTypes.Equipment] as EquipmentData).Slots.ContainsKey(FantasyEquipmentSlotTypes.FootSlot) &&
                (y.Variables[GenreEntityVariableTypes.Equipment] as EquipmentData).Slots.ContainsKey(FantasyEquipmentSlotTypes.WristSlot) &&
                (y.Variables[GenreEntityVariableTypes.Equipment] as EquipmentData).Slots.ContainsKey(FantasyEquipmentSlotTypes.MainHandSlot) &&
                (y.Variables[GenreEntityVariableTypes.Equipment] as EquipmentData).Slots.ContainsKey(FantasyEquipmentSlotTypes.OffHandSlot) &&
                (y.Variables[GenreEntityVariableTypes.Equipment] as EquipmentData).Slots.ContainsKey(FantasyEquipmentSlotTypes.NeckSlot) &&
                (y.Variables[GenreEntityVariableTypes.Equipment] as EquipmentData).Slots.ContainsKey(FantasyEquipmentSlotTypes.Ring1Slot) &&
                (y.Variables[GenreEntityVariableTypes.Equipment] as EquipmentData).Slots.ContainsKey(FantasyEquipmentSlotTypes.Ring2Slot) )));
        }
    }
}