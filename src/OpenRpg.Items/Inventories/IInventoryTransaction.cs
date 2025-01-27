using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Inventories
{
    /// <summary>
    /// This provides a way to make multiple changes to an inventory in a single action, where it either all succeeds
    /// or all fails and no changes are made.
    /// </summary>
    public interface IInventoryTransaction
    {
        public bool ApplyChanges();
        
        void AddItem(ItemData itemData);
        void RemoveItem(ItemData itemData);
    }
}