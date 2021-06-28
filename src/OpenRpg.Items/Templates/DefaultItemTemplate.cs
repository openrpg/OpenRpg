using System;
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
        
        public IEnumerable<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
        public IEnumerable<Effect> Effects { get; set; } = Array.Empty<Effect>();
        public IEnumerable<ModificationAllowance> ModificationAllowances { get; set; } = Array.Empty<ModificationAllowance>();
        public IItemTemplateVariables Variables { get; set; } = new DefaultItemTemplateVariables();
    }
}