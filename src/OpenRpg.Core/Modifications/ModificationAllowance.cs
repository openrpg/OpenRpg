namespace OpenRpg.Core.Modifications
{
    public class ModificationAllowance
    {
        public int AmountAllowed { get; }
        public int ModificationType { get; }

        public ModificationAllowance(int modificationType, int amountAllowed)
        {
            AmountAllowed = amountAllowed;
            ModificationType = modificationType;
        }
    }
}