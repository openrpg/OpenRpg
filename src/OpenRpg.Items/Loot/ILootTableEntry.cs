using OpenRpg.Core.Requirements;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Loot
{
    public interface ILootTableEntry : IHasRequirements, IHasVariables<ILootTableEntryVariables>
    {
        IItemTemplateInstance Item { get; }
    }
}