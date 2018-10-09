namespace OpenRpg.Items.Modifications
{
    public interface IModificationAllowance
    {
        int AmountAllowed { get; }
        int ModificationType { get; }
    }
}