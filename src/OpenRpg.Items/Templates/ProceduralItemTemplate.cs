using System;
using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Modifications;
using OpenRpg.Entities.Requirements;
using OpenRpg.Items.Variables;
using Range = OpenRpg.Core.Utils.Range;

namespace OpenRpg.Items.Templates
{
    public class ProceduralItemTemplate : ITemplate<ItemTemplateVariables>, IHasRequirements, IAllowsModification
    {
        public int Id { get; set; }
        public int ItemType { get; set;  }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }

        public Range EffectRange { get; set; }
        public IReadOnlyList<IEffect> PossibleEffects { get; set; } = Array.Empty<IEffect>();
        
        public IReadOnlyCollection<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
        public IReadOnlyCollection<ModificationAllowance> ModificationAllowances { get; set; } = Array.Empty<ModificationAllowance>();
        public ItemTemplateVariables Variables { get; set; } = new ItemTemplateVariables();
    }
}