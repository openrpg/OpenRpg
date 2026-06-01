using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Loot
{
    public class LootTableEntry : IHasVariables<LootTableEntryVariables>
    {
        public LootTableEntryVariables Variables { get; set; } = new();
        public ItemData ItemData { get; set; }
    }
}