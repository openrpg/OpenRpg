using System.Collections.Generic;
using System.Linq;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Types;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Inventory
{
    public class DefaultInventory : IInventory
    {
        private readonly List<IItem> _items = new List<IItem>();

        public IItem GetItem(int index)
        { return _items[index]; }

        public IReadOnlyCollection<IItem> Items => _items;

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
            
            if (item.ItemTemplate.Variables.ContainsKey(ItemTemplateVariableTypes.MaxStacks))
            { return AttemptAddAmountItem(item); }

            if (item.ItemTemplate.Variables.ContainsKey(ItemTemplateVariableTypes.Weight))
            { return HasWeightCapacity(item.ItemTemplate.Variables.Weight()) && AttemptAddWeightedItem(item); }

            if (!HasSlotCapacity())
            { return false; }

            _items.Add(item);
            return true;
        }

        private bool AttemptAddAmountItem(IItem itemToAdd)
        {
            if (!itemToAdd.Variables.ContainsKey(DefaultItemVariableTypes.Amount))
            {
                if (!HasSlotCapacity())
                { return false; }

                _items.Add(itemToAdd);
                return true;
            }

            var currentMinimumStackItem = _items
                .Where(x => x.ItemTemplate.Id == itemToAdd.ItemTemplate.Id)
                .OrderBy(x => x.Variables.Amount())
                .Take(1)
                .SingleOrDefault();

            if (currentMinimumStackItem == null)
            {
                if (!HasSlotCapacity())
                { return false; }

                _items.Add(itemToAdd);
                return true;
            }

            var maxStack = itemToAdd.ItemTemplate.Variables.MaxStacks();
            var newTotalAmount = currentMinimumStackItem.Variables.Amount() + itemToAdd.Variables.Amount();
            if (newTotalAmount <= maxStack)
            {
                currentMinimumStackItem.Variables.Amount(newTotalAmount);
                return true;
            }

            if (!HasSlotCapacity())
            { return false; }

            var remainder = newTotalAmount - maxStack;
            currentMinimumStackItem.Variables.Amount(maxStack);
            itemToAdd.Variables.Amount(remainder);
            _items.Add(itemToAdd);
            return true;
        }

        private bool AttemptAddWeightedItem(IItem item)
        {
            // TODO
            return false;
        }

        private bool HasSlotCapacity()
        {
            if (!Variables.ContainsKey(InventoryVariableTypes.MaxSlots))
            { return true; }

            return _items.Count < (int)Variables.MaxSlots();
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
            if (itemToRemove.ItemTemplate.Variables.ContainsKey(ItemTemplateVariableTypes.MaxStacks))
            { return AttemptRemoveAmountItem(itemToRemove); }

            if (itemToRemove.ItemTemplate.Variables.ContainsKey(ItemTemplateVariableTypes.Weight))
            { return AttemptRemoveWeightedItem(itemToRemove); }

            if (!_items.Contains(itemToRemove))
            { return false; }

            _items.Remove(itemToRemove);
            return true;
        }

        private bool AttemptRemoveAmountItem(IItem itemToRemove)
        {
            if (!itemToRemove.Variables.ContainsKey(DefaultItemVariableTypes.Amount))
            {
                if (_items.Contains(itemToRemove))
                {
                    _items.Remove(itemToRemove);
                    return true;
                }

                return false;
            }

            var currentMaximumStackItem = _items
                .Where(x => x.ItemTemplate.Id == itemToRemove.ItemTemplate.Id)
                .OrderByDescending(x => x.Variables.Amount())
                .Take(1)
                .SingleOrDefault();

            if (currentMaximumStackItem == null)
            { return false; }

            var newAmount = currentMaximumStackItem.Variables.Amount() - 1;
            if (newAmount <= 0)
            {
                _items.Remove(itemToRemove);
                return true;
            }

            currentMaximumStackItem.Variables.Amount(newAmount);
            return true;
        }

        private bool AttemptRemoveWeightedItem(IItem item)
        {
            // TODO
            return false;
        }
    }
}