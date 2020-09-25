using System.Collections.Generic;

namespace OpenRpg.Items.Inventory
{
    public interface IInventory
    {
        bool AddItem(IItem itemToAdd);
        bool RemoveItem(IItem itemToRemove);
        IItem GetItem(int index);
        
        IReadOnlyCollection<IItem> Items { get; }
        IInventoryVariables Variables { get; }
    }
}