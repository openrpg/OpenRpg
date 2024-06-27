using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Equipment
{
    public interface IEquipmentSlotInstance
    {
        int SlotType { get; set; }
        IItemTemplateInstance Item { get; set; }
    }
}