using System.Collections.Generic;
using System.Linq;
using OpenRpg.Items.Inventory;
using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Extensions
{
    public static class InventoryExtensions
    {
        /// <summary>
        /// Checks if the inventory has the item type
        /// </summary>
        /// <param name="inventory">The inventory to check</param>
        /// <param name="itemTemplate">The item template to check for</param>
        /// <returns>true if has any, false if not</returns>
        /// <remarks>The same functionality is contained within HasItems but this ONLY checks type not amounts/weight</remarks>
        public static bool HasItem(this IInventory inventory, IItemTemplate itemTemplate)
        { return HasItem(inventory, itemTemplate.Id); }
        
        /// <summary>
        /// Checks if the inventory has the item type
        /// </summary>
        /// <param name="inventory">The inventory to check</param>
        /// <param name="itemTemplateId">The item template id to check for</param>
        /// <returns>true if has any, false if not</returns>
        /// <remarks>The same functionality is contained within HasItems but this ONLY checks type not amounts/weight</remarks>
        public static bool HasItem(this IInventory inventory, int itemTemplateId)
        { return inventory.Items.Any(x => x.Instance.TemplateId == itemTemplateId); }
        
        /// <summary>
        /// Checks that you have at least the given amount of an item in the inventory
        /// </summary>
        /// <param name="inventory">The inventory to check</param>
        /// <param name="itemTemplateId">The item id to check for</param>
        /// <param name="amount">The minimum amount required</param>
        /// <returns>true if has >= to the amount required, false if not</returns>
        /// <remarks>The default inventory has this logic built in for HasItems check, but this may be easier to call</remarks>
        public static bool HasItem(this IInventory inventory, int itemTemplateId, int amount)
        {
            var itemAmounts= inventory.Items
                .Where(x => x.Instance.TemplateId == itemTemplateId)
                .Sum(x => x.Instance.Variables.Amount());
            
            return itemAmounts >= amount;
        }
        
        /// <summary>
        /// Removes the amount of an item from the inventory
        /// </summary>
        /// <param name="inventory">The inventory to check</param>
        /// <param name="itemTemplateId">The item template id to check for</param>
        /// <param name="amount">The amount to be removed</param>
        /// <returns>true if it can be removed, false if not</returns>
        public static bool RemoveItem(this IInventory inventory, int itemTemplateId, int amount)
        {
            var proxyItem = new DefaultItemTemplateInstance() { TemplateId = itemTemplateId  };
            proxyItem.Variables.Amount(amount);
            return inventory.RemoveItem(proxyItem);
        }
        
        /// <summary>
        /// Removes the item from the inventory
        /// </summary>
        /// <param name="inventory">The inventory to check</param>
        /// <param name="itemTemplateId">The item template id to check for</param>
        /// <returns>true if it can be removed, false if not</returns>
        public static bool RemoveItem(this IInventory inventory, int itemTemplateId)
        {
            var proxyItem = new DefaultItemTemplateInstance() { TemplateId = itemTemplateId };
            return inventory.RemoveItem(proxyItem);
        }

        /// <summary>
        /// Creates a transaction for the inventory to allow for multiple adds and removals at once
        /// </summary>
        /// <param name="inventory">The inventory to carry out the transaction on</param>
        /// <returns></returns>
        public static IInventoryTransaction CreateTransaction(this IInventory inventory)
        { return new InventoryTransaction(inventory); }
        
        /// <summary>
        /// Checks that you have at least the given amount of an item in the inventory
        /// </summary>
        /// <param name="inventory">The inventory to check</param>
        /// <param name="itemTemplateId">The item id to check for</param>
        /// <param name="amount">The minimum amount required</param>
        /// <returns>true if has >= to the amount required, false if not</returns>
        /// <remarks>The default inventory has this logic built in for HasItems check, but this may be easier to call</remarks>
        public static bool HasItem(this IInventory inventory, int itemTemplateId, float weight)
        {
            var itemWeights= inventory.Items
                .Where(x => x.Instance.TemplateId == itemTemplateId)
                .Sum(x => x.Instance.Variables.HasAmount() 
                    ? x.Instance.Variables.Weight() * x.Instance.Variables.Amount()
                    : x.Instance.Variables.Weight());
            
            return itemWeights >= weight;
        }
        
        /// <summary>
        /// Checks to see if the given item exists in the inventory
        /// </summary>
        /// <param name="itemTemplate">The item to check for</param>
        /// <returns>True if it has the item false if not</returns>
        /// <remarks>Implementations may also factor in variables like amounts/weights etc</remarks>
        public static bool HasItem(this IInventory inventory, IItemTemplateInstance itemTemplate)
        {
            if (itemTemplate.Variables.HasAmount())
            { return inventory.HasItem(itemTemplate.TemplateId, itemTemplate.Variables.Amount()); }

            if (itemTemplate.Variables.HasWeight())
            { return inventory.HasItem(itemTemplate.TemplateId, itemTemplate.Variables.Weight()); }

            return inventory.HasItem(itemTemplate.TemplateId);
        }
        
        /// <summary>
        /// Gets items of the same type as the item template
        /// </summary>
        /// <param name="inventory">The inventory to check</param>
        /// <param name="itemTemplate">The item template to check for</param>
        /// <returns>All items of the given type</returns>
        public static IEnumerable<IItem> GetItemsOfType(this IInventory inventory, IItemTemplate itemTemplate)
        { return GetItemsOfType(inventory, itemTemplate.Id); }

        /// <summary>
        /// Gets items from the inventory by an Id
        /// </summary>
        /// <param name="inventory">The inventory to check</param>
        /// <param name="itemId">The item id to check for</param>
        /// <returns>All items of the given type</returns>
        public static IEnumerable<IItem> GetItemsOfType(this IInventory inventory, int itemTemplateId)
        { return inventory.Items.Where(x => x.Instance.TemplateId == itemTemplateId); }
    }
}