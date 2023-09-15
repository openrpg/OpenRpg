using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventory;
using OpenRpg.Items.TradeSkills.Crafting;

namespace OpenRpg.Items.TradeSkills.Extensions
{
    public static class InventoryExtensions
    {
        public static bool HasItemsRequiredFor(this IInventory inventory, ItemCraftingTemplate craftingTemplate)
        {
            foreach (var itemEntry in craftingTemplate.InputItems)
            {
                if (itemEntry.Variables.HasAmount())
                {
                    var amount = itemEntry.Variables.Amount();
                    if (!inventory.HasItem(itemEntry.ItemTemplateId, amount))
                    { return false; }
                }
                else if (itemEntry.Variables.HasWeight())
                {
                    var weight = itemEntry.Variables.Weight();
                    if (!inventory.HasItem(itemEntry.ItemTemplateId, weight))
                    { return false; }
                }
                else
                {
                    if (!inventory.HasItem(itemEntry.ItemTemplateId))
                    { return false; }
                }
            }

            return true;
        }
    }
}