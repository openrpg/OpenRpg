using System.Collections.Generic;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Modifications;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Templates
{
    public class ProceduralItemData : ITemplateData<ItemVariables>, IHasModifications<ItemModificationData>
    {
        public int TemplateId { get; set; }
        
        public IEnumerable<ItemModificationData> Modifications { get; set;} = new List<ItemModificationData>();
        public ItemVariables Variables { get; set; } = new ItemVariables();
    }
}