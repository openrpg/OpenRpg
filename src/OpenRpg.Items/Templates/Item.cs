using System.Collections.Generic;
using OpenRpg.Core.Modifications;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Templates
{
    public class Item : IHasTemplateLink, IHasVariables<ItemVariables>, IHasModifications<ItemModificationTemplate>
    {
        public int TemplateId { get; set; }
        
        public IEnumerable<ItemModificationTemplate> Modifications { get; set;} = new List<ItemModificationTemplate>();
        public ItemVariables Variables { get; set; } = new ItemVariables();
    }
}