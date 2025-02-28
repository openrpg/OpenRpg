using System;
using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Modifications;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Templates
{
    public class ItemData : ITemplateData<ItemVariables>, IHasModifications<ItemModificationData>, IIsUnique
    {
        public Guid UniqueId { get; set; }
        public int TemplateId { get; set; }
        
        public IEnumerable<ItemModificationData> Modifications { get; set;} = new List<ItemModificationData>();
        public ItemVariables Variables { get; set; } = new ItemVariables();
    }
}