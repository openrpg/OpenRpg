using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Templates;
using OpenRpg.Items.Inventories;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Extensions
{
    public static class InventoryExtensions
    {
        /// <summary>
        /// Checks if the inventory has the item type
        /// </summary>
        /// <param name="inventoryHandler">The inventory to check</param>
        /// <param name="itemTemplateId">The item template id to check for</param>
        /// <returns>true if has any, false if not</returns>
        /// <remarks>The same functionality is contained within HasItems but this ONLY checks type not amounts/weight</remarks>
        public static bool HasItem(this Inventory inventory, int itemTemplateId)
        { return inventory.Items.Any(x => x.TemplateId == itemTemplateId); }
        
        /// <summary>
        /// Checks that you have at least the given amount of an item in the inventory
        /// </summary>
        /// <param name="inventory">The inventory to check</param>
        /// <param name="itemTemplateId">The item id to check for</param>
        /// <param name="amount">The minimum amount required</param>
        /// <returns>true if has >= to the amount required, false if not</returns>
        /// <remarks>The default inventory has this logic built in for HasItems check, but this may be easier to call</remarks>
        public static bool HasItem(this Inventory inventory, int itemTemplateId, int amount)
        {
            var itemAmounts= inventory.Items
                .Where(x => x.TemplateId == itemTemplateId)
                .Sum(x => x.Variables.Amount());
            
            return itemAmounts >= amount;
        }
                 
        /// <summary>
        /// Checks that you have at least the given amount of an item in the inventory
        /// </summary>
        /// <param name="inventory">The inventory to check</param>
        /// <param name="itemTemplateId">The item id to check for</param>
        /// <param name="weight">The minimum weight required</param>
        /// <returns>true if has >= to the amount required, false if not</returns>
        /// <remarks>The default inventory has this logic built in for HasItems check, but this may be easier to call</remarks>
        public static bool HasItem(this Inventory inventory, int itemTemplateId, float weight)
        {
            var itemWeights= inventory.Items
                .Where(x => x.TemplateId == itemTemplateId)
                .Sum(x => x.Variables.HasAmount() 
                    ? x.Variables.Weight() * x.Variables.Amount()
                    : x.Variables.Weight());
            
            return itemWeights >= weight;
        }
        
        /// <summary>
        /// Checks to see if the given item exists in the inventory
        /// </summary>
        /// <param name="itemData">The item to check for</param>
        /// <returns>True if it has the item false if not</returns>
        /// <remarks>Implementations may also factor in variables like amounts/weights etc</remarks>
        public static bool HasItem(this Inventory inventory, ItemData itemData)
        {
            if (itemData.Variables.HasAmount())
            { return inventory.HasItem(itemData.TemplateId, itemData.Variables.Amount()); }

            if (itemData.Variables.HasWeight())
            { return inventory.HasItem(itemData.TemplateId, itemData.Variables.Weight()); }

            return inventory.HasItem(itemData.TemplateId);
        }
        
        /// <summary>
        /// Gets items from the inventory by an Id
        /// </summary>
        /// <param name="inventory">The inventory to check</param>
        /// <param name="itemId">The item id to check for</param>
        /// <returns>All items of the given type</returns>
        public static IEnumerable<ItemData> GetItemsOfType(this Inventory inventory, int itemTemplateId)
        { return inventory.Items.Where(x => x.TemplateId == itemTemplateId); }
        
        public static bool HasSlotCapacity(this Inventory inventory, int slotsRequired = 1)
        {
            if (!inventory.Variables.HasMaxSlots())
            { return true; }

            return inventory.Items.Count + slotsRequired < inventory.Variables.MaxSlots();
        }
        
        public static bool HasWeightCapacity(this Inventory inventory, float weightToAdd)
        {
            if (!inventory.Variables.ContainsKey(InventoryVariableTypes.MaxWeight))
            { return true; }

            var proposedWeight = inventory.Items.Sum(x => x.Variables.Weight()) + weightToAdd;
            return proposedWeight < inventory.Variables.MaxWeight();
        }
        
        /// <summary>
        /// Creates a transaction for the inventory to allow for multiple adds and removals at once
        /// </summary>
        /// <param name="inventory">The inventory to carry out the transaction on</param>
        /// <returns></returns>
        public static IInventoryTransaction CreateTransaction(this Inventory inventory, ITemplateAccessor templateAccessor)
        { return new InventoryTransaction(inventory, templateAccessor); }
        
        /// <summary>
        /// This will attempt to add the item to the inventory in the best way possible, i.e adding to existing stacks/weights
        /// </summary>
        /// <param name="inventory">The inventory you want to alter</param>
        /// <param name="item">The item you want to add, containing both data and template</param>
        /// <returns>true if the item could be added/stacked false if not</returns>
        /// <remarks>
        /// The item passed in should be seen as immutable for the case of this method, in most cases the item
        /// would be cloned internally and assigned (depending on implementation) but the idea is that you can
        /// add items without fear of the source item being modified in any way (i.e quest rewards, loot drops)
        /// as you wouldnt want the source data modified.
        /// 
        /// Depending on the implementation different metadata would be factored in by the default version supports
        /// amount, max stacking, max slots, weight, max weight scenarios.
        /// </remarks>
        public static bool AttemptAddItem(this Inventory inventory, Item item)
        {
            
            if (item.Data.Variables.HasAmount())
            { return AttemptAddAmountItem(inventory, item); }

            if (item.Data.Variables.HasWeight())
            { return HasWeightCapacity(inventory, item.Template.Variables.Weight()) && AttemptAddWeightedItem(inventory, item); }

            if (!HasSlotCapacity(inventory))
            { return false; }

            inventory.Items.Add(item.Data.Clone());
            return true;
        }

        public static bool AttemptAddAmountItem(Inventory inventory, Item item)
        {
            var requiredAmount = item.Data.Variables.Amount();
            var stackSize = item.Template.Variables.MaxStacks();
            
            var existingItemsWithSpace = inventory.Items
                .Where(x => x.TemplateId == item.Data.TemplateId && (stackSize == 0 || x.Variables.Amount() <= stackSize))
                .OrderByDescending(x => x.Variables.Amount())
                .ToArray();

            var maxSlots = inventory.Variables.MaxSlots();
            if (maxSlots > 0)
            {
                var currentSlots = inventory.Items.Count;
                
                if (stackSize > 0)
                {
                    var availableSpace = existingItemsWithSpace.Sum(x => stackSize - x.Variables.Amount());
                    var overflowAmount = requiredAmount - availableSpace;
                    var stacksRequired = (int)Math.Ceiling((float)overflowAmount / stackSize);
                    if(currentSlots + stacksRequired > maxSlots)
                    { return false; }
                }
                else
                {
                    if(currentSlots >= maxSlots)
                    { return false; }
                }
            }
            
            var amountLeft = requiredAmount;
            var index = 0;
            while (amountLeft > 0)
            {
                ItemData itemDataHasWithSpace;
                if (index < existingItemsWithSpace.Length)
                { itemDataHasWithSpace = existingItemsWithSpace[index]; }
                else
                {
                    itemDataHasWithSpace = new ItemData() { TemplateId = item.Data.TemplateId };
                    inventory.Items.Add(itemDataHasWithSpace);
                }

                var existingAmount = itemDataHasWithSpace.Variables.HasAmount()
                    ? itemDataHasWithSpace.Variables.Amount()
                    : 0;
                
                if (stackSize > 0)
                {
                    var spaceLeft = stackSize - existingAmount;
                    if (amountLeft < spaceLeft)
                    {
                        itemDataHasWithSpace.Variables.Amount(existingAmount + amountLeft);
                        amountLeft = 0;
                    }
                    else
                    {
                        itemDataHasWithSpace.Variables.Amount(existingAmount + spaceLeft);
                        amountLeft -= spaceLeft;
                    }
                }
                else
                {
                    itemDataHasWithSpace.Variables.Amount(existingAmount + amountLeft);
                    amountLeft = 0;
                }
                

                index++;
            }

            return true;
        }

        /// <summary>
        /// Attempts to remove the item from the inventory in the best way possible, i.e removing from existing stacks/weights
        /// </summary>
        /// <param name="inventory">The inventory you want to alter</param>
        /// <param name="itemData">The item you want to remove</param>
        /// <returns>true if the item could be removed false if not (i.e it isnt in the inventory)</returns>
        public static bool AttemptRemoveItem(this Inventory inventory, ItemData itemData)
        {
            if (itemData.Variables.HasAmount())
            { return AttemptRemoveAmountItem(inventory, itemData); }

            if (itemData.Variables.HasWeight())
            { return AttemptRemoveWeightedItem(inventory, itemData); }

            if (!inventory.Items.Contains(itemData))
            { return false; }

            inventory.Items.Remove(itemData);
            return true;
        }

        private static bool AttemptRemoveAmountItem(Inventory inventory, ItemData itemData)
        {
            if (!itemData.Variables.HasAmount())
            {
                if (!inventory.Items.Contains(itemData)) { return false; }
                inventory.Items.Remove(itemData);
                return true;
            }

            var amountToTake = itemData.Variables.Amount();
            var applicableItems = inventory.Items
                .Where(x => x.TemplateId == itemData.TemplateId)
                .OrderByDescending(x => x.Variables.Amount())
                .ToArray();

            var maxAvailable = applicableItems.Sum(x => x.Variables.Amount());
            if (maxAvailable < amountToTake)
            { return false; }

            var index = 0;
            while (amountToTake > 0)
            {
                var currentItem = applicableItems[index];
                var itemAmount = currentItem.Variables.Amount();
                if (amountToTake >= itemAmount)
                {
                    inventory.Items.Remove(currentItem);
                    amountToTake -= itemAmount;
                }
                else
                {
                    currentItem.Variables.Amount(itemAmount - amountToTake);
                    amountToTake -= itemAmount;
                }
                index++;
            }
            
            return true;
        }

        private static bool AttemptRemoveWeightedItem(Inventory inventory, ItemData itemData)
        {
            // TODO
            return false;
        }
        
        private static bool AttemptAddWeightedItem(Inventory inventory, Item item)
        {
            // TODO
            return false;
        }
    }
}