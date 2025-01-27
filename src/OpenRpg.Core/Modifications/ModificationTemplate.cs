using System;
using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Templates;

namespace OpenRpg.Core.Modifications
{
    public class ModificationTemplate : ITemplate, IHasEffects
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }

        public IReadOnlyCollection<Effect> Effects { get; set; } = Array.Empty<Effect>();
        public int ModificationType { get; set; }
    }
}