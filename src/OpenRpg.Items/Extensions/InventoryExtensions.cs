using System.Collections.Generic;
using System.Linq;
using OpenRpg.Items.Inventory;
using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Extensions
{
    public static class InventoryExtensions
    {
        public static bool HasItemsOfType(this IInventory inventory, IItemTemplate itemTemplate)
        {
            var comparisonItem = new DefaultItem() { ItemTemplate = itemTemplate };
            return inventory.HasItem(comparisonItem);
        }
        
        public static IEnumerable<IItem> GetItemsOfType(this IInventory inventory, IItemTemplate itemTemplate)
        {
            return inventory.Items
                .Where(x => x.ItemTemplate.Id == itemTemplate.Id);
        }
    }
}