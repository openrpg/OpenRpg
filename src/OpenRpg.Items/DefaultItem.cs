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

        public IItemTemplate ItemTemplate { get; set; } = new DefaultItemTemplate();
        public IEnumerable<IModification> Modifications { get; set;} = new List<IModification>();
        public IItemVariables Variables { get; set; } = new DefaultItemVariables();
    }
}