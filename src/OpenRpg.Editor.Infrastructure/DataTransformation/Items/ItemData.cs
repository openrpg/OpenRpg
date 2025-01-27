using System.Collections.Generic;

namespace OpenRpg.Editor.Infrastructure.DataTransformation.Items
{
    public class InventoryItemData
    {
        public int Amount { get; set; }
        public int ItemTemplateId { get; set; }
        public List<int> ModificationId { get; set; }
    }
}