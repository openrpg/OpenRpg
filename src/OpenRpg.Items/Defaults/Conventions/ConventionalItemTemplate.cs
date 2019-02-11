using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Modifications;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Items.Defaults.Conventions
{
    public class ConventionalItemTemplate : IConventionalItemTemplate
    {
        public int Id { get; set; }
        public int ItemType { get; set; }
        public int ItemQualityType { get; set; }
        public int ItemValue { get; set; }
        public int StackableAmount { get; set; }
        public string AssetCode { get; set; }
        public string NameLocaleId => $"{AssetCode}-name";
        public string DescriptionLocaleId => $"{AssetCode}-description";
        
        public IEnumerable<Requirement> Requirements { get; set; } = new List<Requirement>();
        public IEnumerable<Effect> Effects { get; set; } = new List<Effect>();
        public IEnumerable<ModificationAllowance> ModificationAllowances { get; set; } = new List<ModificationAllowance>();
    }
}