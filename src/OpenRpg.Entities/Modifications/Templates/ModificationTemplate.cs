using System;
using System.Collections.Generic;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Effects;

namespace OpenRpg.Entities.Modifications.Templates
{
    public class ModificationTemplate : ITemplate, IHasEffects
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }

        public IReadOnlyCollection<IEffect> Effects { get; set; } = Array.Empty<IEffect>();
        public int ModificationType { get; set; }
    }
}