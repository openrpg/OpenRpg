using System.Collections.Generic;
using OpenRpg.Core.Modifications;
using OpenRpg.Items.Defaults.Conventions;
using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Defaults
{
    public class DefaultItem : IItem
    {
        public string NameOverride { get; set; }
        public int Amount { get; set; }
        public IItemTemplate ItemTemplate { get; set; } = new ConventionalItemTemplate();
        public IEnumerable<IModification> Modifications { get; set;} = new List<IModification>();
    }
}