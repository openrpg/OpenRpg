using System;
using System.Collections.Generic;
using OpenRpg.Core.Classes.Variables;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Core.Classes
{
    public class DefaultClassTemplate : IClassTemplate
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public IEnumerable<Effect> Effects { get; set; } = Array.Empty<Effect>();
        public IEnumerable<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
        public IClassTemplateVariables Variables { get; set; } = new DefaultClassTemplateVariables();
    }
}