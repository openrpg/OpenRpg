using OpenRpg.Entities.Modifications;

namespace OpenRpg.Items.Templates
{
    public class ItemModificationData : ModificationData
    {
        public ItemModificationData() : base() { }
        public ItemModificationData(int templateId) : base(templateId) { }
    }
}