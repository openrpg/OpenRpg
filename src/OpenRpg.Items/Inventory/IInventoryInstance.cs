using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Inventory
{
    public interface IInventoryInstance : IHasVariables<IInventoryVariables>
    {
        public List<IItemTemplateInstance> Items { get; set; }
    }
}