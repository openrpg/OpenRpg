using OpenRpg.Core.Requirements;

namespace OpenRpg.Items.Loot
{
    public interface ILootTableEntry : IHasRequirements
    {
        float DropRate { get; }
        bool IsUnique { get; }
        IItem Item { get; }
    }
}