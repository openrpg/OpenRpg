namespace OpenRpg.Core.Modifications
{
    public class ModificationAllowance
    {
        public int AmountAllowed { get; }
        public int ModificationType { get; }

        public ModificationAllowance(int amountAllowed, int modificationType)
        {
            AmountAllowed = amountAllowed;
            ModificationType = modificationType;
        }
    }
}