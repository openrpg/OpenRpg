using System.Linq;
using OpenRpg.Core.Templates;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventories;
using OpenRpg.Items.TradeSkills.Templates;

namespace OpenRpg.Items.TradeSkills.Extensions
{
    public static class InventoryExtensions
    {
        public static bool HasItemsRequiredFor(this Inventory inventory, ItemCraftingTemplate craftingTemplate)
        {
            return craftingTemplate.InputItems
                .Select(x => x.AsItem())
                .All(inventory.HasItem);
        }
        
        /// <summary>
        /// This attempts to craft the item into the inventory, it only supports amounts currently
        /// </summary>
        /// <param name="craftingTemplate">The template containing the input and output items</param>
        /// <param name="inventory">The inventory to craft from/into</param>
        /// <param name="templateAccessor">The template accessor to get templates for items</param>
        /// <returns>true if succeeded, false if failed</returns>
        /// <remarks>An inventory could be full so most false returns are due to full inventories</remarks>
        public static bool CraftFrom(this ItemCraftingTemplate craftingTemplate, Inventory inventory, ITemplateAccessor templateAccessor)
        {
            return inventory.CreateTransaction(templateAccessor)
                .AddItems(craftingTemplate.OutputItems.Select(x => x.AsItem()))
                .RemoveItems(craftingTemplate.InputItems.Select(x => x.AsItem()))
                .ApplyChanges();
        }

        /// <summary>
        /// This attempts to gather items into the inventory, it only supports amounts currently
        /// </summary>
        /// <param name="gatheringTemplate">The template containing the output items</param>
        /// <param name="inventory">The inventory to gather into</param>
        /// <param name="templateAccessor">The template accessor to get templates for items</param>
        /// <returns>true if succeeded, false if failed</returns>
        /// <remarks>An inventory could be full so most false returns are due to full inventories</remarks>
        public static bool GatherInto(this ItemGatheringTemplate gatheringTemplate, Inventory inventory, ITemplateAccessor templateAccessor)
        {
            return inventory.CreateTransaction(templateAccessor)
                .AddItems(gatheringTemplate.OutputItems.Select(x => x.AsItem()))
                .ApplyChanges();
        }
    }
}