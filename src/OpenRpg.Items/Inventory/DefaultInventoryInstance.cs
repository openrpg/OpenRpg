using System.Collections.Generic;
using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Inventory
{
    public class DefaultInventoryInstance : IInventoryInstance
    {
        public IInventoryVariables Variables { get; set; } = new DefaultInventoryVariables();
        public List<IItemTemplateInstance> Items { get; set; } = new List<IItemTemplateInstance>();
    }
}