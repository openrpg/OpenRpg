using System;
using System.Collections.Generic;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Modifications;
using OpenRpg.Entities.Requirements;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Templates
{
    public class ItemTemplate : ITemplate<ItemTemplateVariables>, IHasRequirements, IHasEffects, IAllowsModification
    {
        public int Id { get; set; }
        public int ItemType { get; set;  }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        
        public IReadOnlyCollection<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
        public IReadOnlyCollection<StaticEffect> Effects { get; set; } = Array.Empty<StaticEffect>();
        public IReadOnlyCollection<ModificationAllowance> ModificationAllowances { get; set; } = Array.Empty<ModificationAllowance>();
        public ItemTemplateVariables Variables { get; set; } = new ItemTemplateVariables();
    }
}