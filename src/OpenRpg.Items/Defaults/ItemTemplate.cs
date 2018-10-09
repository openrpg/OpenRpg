using System.Collections.Generic;
using OpenRpg.Core.Requirements;
using OpenRpg.Effects;
using OpenRpg.Items.Modifications;

namespace OpenRpg.Items.Defaults
{
    public class ItemTemplate : IItemTemplate
    {
        public int Id { get; set; }
        public int Type { get; }
        public int Value { get; set; }
        public string AssetCode { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public IEnumerable<Requirement> Requirements { get; set; }
        public IEnumerable<Effect> Effects { get; set; }
        public IEnumerable<IModificationAllowance> ModificationAllowances { get; set; }
    }
}