namespace OpenRpg.Items
{
    public interface IStackableItemTemplate : IItemTemplate
    {
        int StackableAmount { get; }
    }
}