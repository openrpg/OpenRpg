using System.Collections.Generic;
using System.Linq;
using OpenRpg.Items.Extensions;

namespace OpenRpg.Items.Inventory
{
    public class InventoryTransaction : IInventoryTransaction
    {
        public IInventory Inventory { get; }

        public List<IItem> Additions { get; } = new List<IItem>();
        public List<IItem> Removals { get; } = new List<IItem>();

        public void AddItem(IItem item) => Additions.Add(item);
        public void RemoveItem(IItem item) => Removals.Add(item);

        public InventoryTransaction(IInventory inventory)
        {
            Inventory = inventory;
        }

        public void RevertRemovalsUpTo(int lastProcessedIndex)
        {
            for (var i = 0; i <= lastProcessedIndex; i++)
            { Inventory.AddItem(Removals[i]); }
        }
        
        public void RevertAdditionsUpTo(int lastProcessedIndex)
        {
            for (var i = 0; i <= lastProcessedIndex; i++)
            { Inventory.RemoveItem(Additions[i]); }
        }
        
        public bool ApplyChanges()
        {
            var hasAllItems = Removals.All(Inventory.HasItem);
            if (!hasAllItems) { return false; } 

            for (var i = 0; i < Removals.Count; i++)
            {
                var itemToRemove = Removals[i];
                if (!Inventory.RemoveItem(itemToRemove))
                { 
                    RevertRemovalsUpTo(i);
                    return false;
                }
            }

            for (var i = 0; i < Additions.Count; i++)
            {
                if (!Inventory.AddItem(Additions[i]))
                {
                    RevertAdditionsUpTo(i);
                    RevertRemovalsUpTo(Removals.Count-1);
                    return false;
                }
            }

            return true;
        }
    }
}