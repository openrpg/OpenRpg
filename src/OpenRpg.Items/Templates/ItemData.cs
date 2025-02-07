using System.Collections.Generic;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Modifications;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Templates
{
    public class ItemData : ITemplateData<ItemVariables>, IHasModifications<ItemModificationTemplate>
    {
        public int TemplateId { get; set; }
        
        public IEnumerable<ItemModificationTemplate> Modifications { get; set;} = new List<ItemModificationTemplate>();
        public ItemVariables Variables { get; set; } = new ItemVariables();
    }
}