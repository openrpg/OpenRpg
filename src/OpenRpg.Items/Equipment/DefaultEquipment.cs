using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Equipment
{
    public class DefaultEquipment : IEquipment
    {
        public IEquipmentSlots Slots { get; set; } = new DefaultEquipmentSlots();
        public IEquipmentVariables Variables { get; set; } = new DefaultEquipmentVariables();
    }
}