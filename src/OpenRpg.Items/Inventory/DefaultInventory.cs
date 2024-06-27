using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Templates;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Types;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Inventory
{
    public class DefaultInventory : IInventory
    {
        public ITemplateAccessor TemplateAccessor { get; }
        public IInventoryInstance InventoryInstance { get; }
        
        public IEnumerable<IItem> Items => InventoryInstance.Items.Select(x => new DefaultItem { Instance = x, Template = TemplateAccessor.Get<IItemTemplate>(x.TemplateId)});

        public DefaultInventory(ITemplateAccessor templateAccessor, IInventoryInstance inventoryInstance)
        {
            TemplateAccessor = templateAccessor;
            InventoryInstance = inventoryInstance;
        }
        
        // In future versions this will probably be split out into a framework level notion
        protected virtual IItemTemplateInstance CloneItem(IItemTemplateInstance itemTemplate)
        {
            var duplicatedVariables = new DefaultItemVariables();

            if(itemTemplate.Variables.HasAmount())
            { duplicatedVariables.Amount(itemTemplate.Variables.Amount()); }
            
            return new DefaultItemTemplateInstance
            {
                Modifications = itemTemplate.Modifications.ToArray(),
                TemplateId = itemTemplate.TemplateId,
                Variables = duplicatedVariables
            };
        }

        public bool AddItem(IItemTemplateInstance itemInstance)
        {
            var item = CloneItem(itemInstance);
            
            if (item.Variables.HasAmount())
            { return AttemptAddAmountItem(item); }

            if (item.Variables.HasWeight())
            {
                var template = TemplateAccessor.Get<IItemTemplate>(itemInstance.TemplateId);
                return HasWeightCapacity(template.Variables.Weight()) && AttemptAddWeightedItem(item);
            }

            if (!HasSlotCapacity())
            { return false; }

            InventoryInstance.Items.Add(item);
            return true;
        }

        private bool AttemptAddAmountItem(IItemTemplateInstance itemTemplateToAdd)
        {
            var requiredAmount = itemTemplateToAdd.Variables.Amount();
            var template = TemplateAccessor.Get<IItemTemplate>(itemTemplateToAdd.TemplateId);
            var stackSize = template.Variables.MaxStacks();
            
            var existingItemsWithSpace = InventoryInstance.Items
                .Where(x => x.TemplateId == itemTemplateToAdd.TemplateId && (stackSize == 0 || x.Variables.Amount() <= stackSize))
                .OrderByDescending(x => x.Variables.Amount())
                .ToArray();

            var maxSlots = InventoryInstance.Variables.MaxSlots();
            if (maxSlots > 0)
            {
                var currentSlots = InventoryInstance.Items.Count;
                
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
                IItemTemplateInstance itemWithSpace;
                if (index < existingItemsWithSpace.Length)
                { itemWithSpace = existingItemsWithSpace[index]; }
                else
                {
                    itemWithSpace = new DefaultItemTemplateInstance() { TemplateId = itemTemplateToAdd.TemplateId };
                    InventoryInstance.Items.Add(itemWithSpace);
                }

                var existingAmount = itemWithSpace.Variables.HasAmount()
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
            if (!InventoryInstance.Variables.HasMaxSlots())
            { return true; }

            return InventoryInstance.Items.Count < InventoryInstance.Variables.MaxSlots();
        }

        private bool HasWeightCapacity(float weightToAdd)
        {
            if (!InventoryInstance.Variables.ContainsKey(InventoryVariableTypes.MaxWeight))
            { return true; }

            var proposedWeight = Items.Sum(x => x.Template.Variables.Weight()) + weightToAdd;
            return proposedWeight < InventoryInstance.Variables.MaxWeight();

        }

        public bool RemoveItem(IItemTemplateInstance itemInstance)
        {
            if (itemInstance.Variables.HasAmount())
            { return AttemptRemoveAmountItem(itemInstance); }

            if (itemInstance.Variables.HasWeight())
            { return AttemptRemoveWeightedItem(itemInstance); }

            if (!InventoryInstance.Items.Contains(itemInstance))
            { return false; }

            InventoryInstance.Items.Remove(itemInstance);
            return true;
        }

        private bool AttemptRemoveAmountItem(IItemTemplateInstance itemTemplateToRemove)
        {
            if (!itemTemplateToRemove.Variables.HasAmount())
            {
                if (!InventoryInstance.Items.Contains(itemTemplateToRemove)) { return false; }
                InventoryInstance.Items.Remove(itemTemplateToRemove);
                return true;
            }

            var amountToTake = itemTemplateToRemove.Variables.Amount();
            var applicableItems = InventoryInstance.Items
                .Where(x => x.TemplateId == itemTemplateToRemove.TemplateId)
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
                    InventoryInstance.Items.Remove(currentItem);
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

        private bool AttemptRemoveWeightedItem(IItemTemplateInstance item)
        {
            // TODO
            return false;
        }
        
        private bool AttemptAddWeightedItem(IItemTemplateInstance item)
        {
            // TODO
            return false;
        }
    }
}