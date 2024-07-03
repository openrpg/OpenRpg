using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Inventories
{
    /// <summary>
    /// The inventory wraps up the responsibility of storing items and allowing them to be added/removed etc
    /// </summary>
    public class Inventory : IHasVariables<InventoryVariables>
    {
        public List<Item> Items { get; set; } = new List<Item>();
        public InventoryVariables Variables { get; set; } = new InventoryVariables();
    }
}