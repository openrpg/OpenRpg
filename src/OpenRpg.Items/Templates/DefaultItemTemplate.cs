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
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        
        public IEnumerable<Requirement> Requirements { get; set; } = new Requirement[0];
        public IEnumerable<Effect> Effects { get; set; } = new Effect[0];
        public IEnumerable<ModificationAllowance> ModificationAllowances { get; set; } = new ModificationAllowance[0];
        public IItemTemplateVariables Variables { get; set; } = new DefaultItemTemplateVariables();
    }
}