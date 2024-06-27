using System;
using System.Collections.Generic;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Templates
{
    public class DefaultItemTemplateInstance : IItemTemplateInstance
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();
        public int TemplateId { get; set; }
        public IEnumerable<IItemModificationTemplate> Modifications { get; set;} = new List<IItemModificationTemplate>();
        public IItemVariables Variables { get; set; } = new DefaultItemVariables();
    }
}