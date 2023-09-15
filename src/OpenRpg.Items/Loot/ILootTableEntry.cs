using OpenRpg.Core.Requirements;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Loot
{
    public interface ILootTableEntry : IHasRequirements
    {
        ILootTableEntryVariables Variables { get; }
        IItem Item { get; }
    }
}