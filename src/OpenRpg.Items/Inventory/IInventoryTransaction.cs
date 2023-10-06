namespace OpenRpg.Items.Inventory
{
    /// <summary>
    /// This provides a way to make multiple changes to an inventory in a single action, where it either all succeeds
    /// or all fails and no changes are made.
    /// </summary>
    public interface IInventoryTransaction
    {
        public bool ApplyChanges();
        
        void AddItem(IItem item);
        void RemoveItem(IItem item);
    }
}