using Moq;
using OpenRpg.Items.Equippables;
using OpenRpg.Items.Equippables.Slots;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Templates;
using Xunit;

namespace OpenRpg.UnitTests.Items.Equippables
{
    public class EquipmentExtensionsTests
    {
        [Fact]
        public void should_equip_item_when_all_criteria_met()
        {
            var equipmentSlot = 1;
            var equipment = new Equipment();
            equipment.Slots[equipmentSlot] = null;
            
            var item = new ItemData();
            var itemTemplate = new ItemTemplate();

            var mockEquipmentValidator = new Mock<IEquipmentSlotValidator>();
            mockEquipmentValidator
                .Setup(x => x.CanEquipItemType(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            var result = equipment.AttemptEquipSlot(mockEquipmentValidator.Object, equipmentSlot, item, itemTemplate);

            Assert.True(result);
            Assert.Equal(equipment.Slots[equipmentSlot], item);
        }

        [Fact]
        public void should_not_equip_item_when_slot_doesnt_exist()
        {
            var equipmentSlot = 1;
            var equipment = new Equipment();
            
            var item = new ItemData();
            var itemTemplate = new ItemTemplate();

            var mockEquipmentValidator = new Mock<IEquipmentSlotValidator>();
            mockEquipmentValidator
                .Setup(x => x.CanEquipItemType(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            var result = equipment.AttemptEquipSlot(mockEquipmentValidator.Object, equipmentSlot, item, itemTemplate);

            Assert.False(result);
            Assert.Empty(equipment.Slots);
        }
        
        [Fact]
        public void should_not_equip_item_when_slot_exists_but_validation_fails()
        {
            var equipmentSlot = 1;
            var equipment = new Equipment();
            equipment.Slots[equipmentSlot] = null;
            
            var item = new ItemData();
            var itemTemplate = new ItemTemplate();

            var mockEquipmentValidator = new Mock<IEquipmentSlotValidator>();
            mockEquipmentValidator
                .Setup(x => x.CanEquipItemType(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);

            var result = equipment.AttemptEquipSlot(mockEquipmentValidator.Object, equipmentSlot, item, itemTemplate);

            Assert.False(result);
            Assert.Null(equipment.Slots[equipmentSlot]);
        }
    }
}