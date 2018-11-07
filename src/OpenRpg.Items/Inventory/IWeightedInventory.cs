namespace OpenRpg.Items.Inventory
{
    public interface IWeightedInventory : IInventory
    {
        float MaxWeight { get; set; }
        float Weight { get; }
    }
}