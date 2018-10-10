namespace OpenRpg.Items.Templates
{
    public interface IWeightedItemTemplate : IItemTemplate
    {
        float Weight { get; }
    }
}