using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Modifications;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Items.Templates
{
    public class DefaultItemTemplate : IItemTemplate
    {
        public int Id { get; set; }
        public int ItemType { get; set;  }
        public string AssetCode { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        
        public IEnumerable<Requirement> Requirements { get; set; } = new List<Requirement>();
        public IEnumerable<Effect> Effects { get; set; } = new List<Effect>();
        public IEnumerable<ModificationAllowance> ModificationAllowances { get; set; } = new List<ModificationAllowance>();
        public IItemTemplateVariables Variables { get; set; } = new DefaultItemTemplateVariables();
    }
}