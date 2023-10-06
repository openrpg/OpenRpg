using System;
using System.Collections.Generic;
using OpenRpg.Core.Modifications;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items
{
    public class DefaultItem : IUniqueItem
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();

        public IItemTemplate Template { get; set; } = new DefaultItemTemplate();
        public IEnumerable<IModificationTemplate> Modifications { get; set;} = new List<IModificationTemplate>();
        public IItemVariables Variables { get; set; } = new DefaultItemVariables();
    }
}