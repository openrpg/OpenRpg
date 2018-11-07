namespace OpenRpg.Items.Inventory
{
    public interface ISlottedInventory : IInventory
    {
        int MaxSlots { get; set; }
    }
}