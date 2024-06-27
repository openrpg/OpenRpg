using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Equipment
{
    public class DefaultEquipmentSlotInstance : IEquipmentSlotInstance
    {
        public int SlotType { get; set; }
        public IItemTemplateInstance Item { get; set; } = new DefaultItemTemplateInstance();
    }
}