using System;
using System.Collections.Generic;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Classes.Variables;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Requirements;

namespace OpenRpg.Entities.Classes.Templates
{
    public class ClassTemplate : ITemplate<ClassTemplateVariables>, IHasEffects, IHasRequirements
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public IReadOnlyCollection<IEffect> Effects { get; set; } = Array.Empty<IEffect>();
        public IReadOnlyCollection<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
        public ClassTemplateVariables Variables { get; set; } = new ClassTemplateVariables();
    }
}