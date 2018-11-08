namespace OpenRpg.Core.Common
{
    public interface IHasAssociation
    {
        int AssociatedId { get; }
        int AssociatedValue { get; }
    }
}