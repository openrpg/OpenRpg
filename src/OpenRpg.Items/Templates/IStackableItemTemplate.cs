namespace OpenRpg.Items.Templates
{
    public interface IStackableItemTemplate : IItemTemplate
    {
        int StackableAmount { get; }
    }
}