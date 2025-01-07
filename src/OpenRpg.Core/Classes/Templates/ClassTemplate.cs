using System;
using System.Collections.Generic;
using OpenRpg.Core.Classes.Variables;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Classes.Templates
{
    public class ClassTemplate : ITemplate<ClassTemplateVariables>, IHasEffects, IHasRequirements
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public IReadOnlyCollection<Effect> Effects { get; set; } = Array.Empty<Effect>();
        public IReadOnlyCollection<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
        public ClassTemplateVariables Variables { get; set; } = new ClassTemplateVariables();
    }
}