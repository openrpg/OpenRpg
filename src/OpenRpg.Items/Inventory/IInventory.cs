using System.Collections.Generic;

namespace OpenRpg.Items.Inventory
{
    public interface IInventory
    {
        bool AddItem(IItem itemToAdd);
        bool RemoveItem(IItem itemToRemove);

        IReadOnlyCollection<IItem> Items { get; }
    }
}