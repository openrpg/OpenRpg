using System;
using System.Collections.Generic;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Effects;

namespace OpenRpg.Entities.Modifications
{
    public class ModificationTemplate : ITemplate, IHasEffects
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }

        public IReadOnlyCollection<StaticEffect> Effects { get; set; } = Array.Empty<StaticEffect>();
        public int ModificationType { get; set; }
    }
}