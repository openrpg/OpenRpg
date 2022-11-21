using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Types;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Inventory
{
    public class DefaultInventory : IInventory
    {
        public List<IItem> InternalItems { get; }

        public DefaultInventory(IEnumerable<IItem> items = null)
        {
            InternalItems = items?.ToList() ?? new List<IItem>();
        }

        public IItem GetItem(int index)
        { return InternalItems[index]; }

        public bool HasItem(IItem item)
        {
            if (item.Variables.ContainsKey(DefaultItemVariableTypes.Amount))
            {
                var totalAmount = InternalItems
                    .Where(x => x.ItemTemplate.Id == item.ItemTemplate.Id)
                    .Sum(x => x.Variables.Amount());
                
                return totalAmount >= item.Variables.Amount();
            }

            if (item.Variables.ContainsKey(DefaultItemVariableTypes.Weight))
            {
                var totalWeight = InternalItems
                    .Where(x => x.ItemTemplate.Id == item.ItemTemplate.Id)
                    .Sum(x => x.Variables.Weight());

                return totalWeight >= item.Variables.Weight();
            }

            return InternalItems.Any(x => x.ItemTemplate.Id == item.ItemTemplate.Id);
        }

        public IReadOnlyCollection<IItem> Items => InternalItems;

        public IInventoryVariables Variables { get; set; } = new DefaultInventoryVariables();

        // In future versions this will probably be split out into a framework level notion
        protected virtual IItem CloneItem(IItem item)
        {
            var duplicatedVariables = new DefaultItemVariables();

            if(item.Variables.ContainsKey(DefaultItemVariableTypes.Amount))
            { duplicatedVariables.Amount(item.Variables.Amount()); }
            
            return new DefaultItem
            {
                Modifications = item.Modifications.ToArray(),
                ItemTemplate = item.ItemTemplate,
                Variables = duplicatedVariables
            };
        }

        public bool AddItem(IItem itemToAdd)
        {
            var item = CloneItem(itemToAdd);
            
            if (item.Variables.ContainsKey(DefaultItemVariableTypes.Amount))
            { return AttemptAddAmountItem(item); }

            if (item.Variables.ContainsKey(DefaultItemVariableTypes.Weight))
            { return HasWeightCapacity(item.ItemTemplate.Variables.Weight()) && AttemptAddWeightedItem(item); }

            if (!HasSlotCapacity())
            { return false; }

            InternalItems.Add(item);
            return true;
        }

        private bool AttemptAddAmountItem(IItem itemToAdd)
        {
            var requiredAmount = itemToAdd.Variables.Amount();
            var stackSize = itemToAdd.ItemTemplate.Variables.MaxStacks();
            var existingItemsWithSpace = InternalItems
                .Where(x => x.ItemTemplate.Id == itemToAdd.ItemTemplate.Id && (stackSize == 0 || x.Variables.Amount() <= stackSize))
                .OrderByDescending(x => x.Variables.Amount())
                .ToArray();

            var maxSlots = Variables.MaxSlots();
            if (maxSlots > 0)
            {
                var currentSlots = InternalItems.Count;
                
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
                IItem itemWithSpace;
                if (index < existingItemsWithSpace.Length)
                { itemWithSpace = existingItemsWithSpace[index]; }
                else
                {
                    itemWithSpace = new DefaultItem() { ItemTemplate = itemToAdd.ItemTemplate };
                    InternalItems.Add(itemWithSpace);
                }

                var existingAmount = itemWithSpace.Variables.ContainsKey(DefaultItemVariableTypes.Amount)
                    ? itemWithSpace.Variables.Amount()
                    : 0;
                
                if (stackSize > 0)
                {
                    var spaceLeft = stackSize - existingAmount;
                    if (amountLeft < spaceLeft)
                    {
                        itemWithSpace.Variables.Amount(existingAmount + amountLeft);
                        amountLeft = 0;
                    }
                    else
                    {
                        itemWithSpace.Variables.Amount(existingAmount + spaceLeft);
                        amountLeft -= spaceLeft;
                    }
                }
                else
                {
                    itemWithSpace.Variables.Amount(existingAmount + amountLeft);
                    amountLeft = 0;
                }
                

                index++;
            }

            return true;
        }
        
        private bool HasSlotCapacity()
        {
            if (!Variables.ContainsKey(InventoryVariableTypes.MaxSlots))
            { return true; }

            return InternalItems.Count < (int)Variables.MaxSlots();
        }

        private bool HasWeightCapacity(float weightToAdd)
        {
            if (!Variables.ContainsKey(InventoryVariableTypes.MaxWeight))
            { return true; }

            var proposedWeight = Items.Sum(x => x.ItemTemplate.Variables.Weight()) + weightToAdd;
            return proposedWeight < Variables.MaxWeight();

        }

        public bool RemoveItem(IItem itemToRemove)
        {
            if (itemToRemove.Variables.ContainsKey(DefaultItemVariableTypes.Amount))
            { return AttemptRemoveAmountItem(itemToRemove); }

            if (itemToRemove.Variables.ContainsKey(DefaultItemVariableTypes.Weight))
            { return AttemptRemoveWeightedItem(itemToRemove); }

            if (!InternalItems.Contains(itemToRemove))
            { return false; }

            InternalItems.Remove(itemToRemove);
            return true;
        }

        private bool AttemptRemoveAmountItem(IItem itemToRemove)
        {
            if (!itemToRemove.Variables.ContainsKey(DefaultItemVariableTypes.Amount))
            {
                if (!InternalItems.Contains(itemToRemove)) { return false; }
                InternalItems.Remove(itemToRemove);
                return true;
            }

            var amountToTake = itemToRemove.Variables.Amount();
            var applicableItems = InternalItems
                .Where(x => x.ItemTemplate.Id == itemToRemove.ItemTemplate.Id)
                .OrderByDescending(x => x.Variables.Amount())
                .ToList();

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
                    InternalItems.Remove(currentItem);
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

        private bool AttemptRemoveWeightedItem(IItem item)
        {
            // TODO
            return false;
        }
        
        private bool AttemptAddWeightedItem(IItem item)
        {
            // TODO
            return false;
        }
    }
}