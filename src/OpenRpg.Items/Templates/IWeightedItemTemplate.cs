namespace OpenRpg.Items
{
    public interface IWeightedItemTemplate : IItemTemplate
    {
        float Weight { get; }
    }
}