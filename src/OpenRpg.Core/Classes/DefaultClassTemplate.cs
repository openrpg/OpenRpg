using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Core.Classes
{
    public class DefaultClassTemplate : IClassTemplate
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public IEnumerable<Effect> Effects { get; set; } = new Effect[0];
        public IEnumerable<Requirement> Requirements { get; set; } = new Requirement[0];
        public IClassTemplateVariables Variables { get; set; } = new DefaultClassTemplateVariables();
    }
}