using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Equipment
{
    public interface IEquipment
    {
        IEquipmentSlots Slots { get; }
        IEquipmentVariables Variables { get; }
    }
}