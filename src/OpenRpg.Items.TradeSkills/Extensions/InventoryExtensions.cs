using System.Linq;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventory;
using OpenRpg.Items.TradeSkills.Crafting;

namespace OpenRpg.Items.TradeSkills.Extensions
{
    public static class InventoryExtensions
    {
        public static bool HasItemsRequiredFor(this IInventory inventory, ItemCraftingTemplate craftingTemplate)
        {
            return craftingTemplate.InputItems
                .Select(x => x.AsItem())
                .All(inventory.HasItem);
        }
        
        /// <summary>
        /// This attempts to craft the item into the inventory, it only supports amounts currently
        /// </summary>
        /// <param name="craftingTemplate"></param>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public static bool CraftFrom(this ItemCraftingTemplate craftingTemplate, IInventory inventory)
        {
            return inventory.CreateTransaction()
                .AddItems(craftingTemplate.OutputItems.Select(x => x.AsItem()))
                .RemoveItems(craftingTemplate.InputItems.Select(x => x.AsItem()))
                .ApplyChanges();
        }
    }
}