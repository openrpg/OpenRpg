using System.Collections.Generic;

namespace OpenRpg.Items.Inventory
{
    public class DefaultInventory : IInventory
    {
        private List<IItem> _items = new List<IItem>();
        
        public IItem GetItem(int index)
        { return _items[index]; }

        public IReadOnlyCollection<IItem> Items => _items;
        
        public IInventoryVariables Variables { get; set; } = new DefaultInventoryVariables();

        public bool AddItem(IItem itemToAdd)
        {
            _items.Add(itemToAdd);
            return true;
        }

        public bool RemoveItem(IItem itemToRemove)
        {
            _items.Remove(itemToRemove);
            return true;
        }
    }
}