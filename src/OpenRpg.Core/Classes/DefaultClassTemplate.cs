using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Core.Classes
{
    public class DefaultClassTemplate : IClassTemplate
    {
        public int Id { get; set; }
        public string AssetCode { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public IEnumerable<Effect> Effects { get; set; }
        public IEnumerable<Requirement> Requirements { get; set; }
        public IClassTemplateVariables Variables { get; set; }
    }
}