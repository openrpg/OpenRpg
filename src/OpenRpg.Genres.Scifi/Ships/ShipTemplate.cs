using System;
using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Requirements;

namespace OpenRpg.Genres.Scifi.Ships
{
    public class ShipTemplate : ITemplate, IHasEffects, IHasRequirements
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public IReadOnlyCollection<IEffect> Effects { get; set; } = Array.Empty<IEffect>();
        public IReadOnlyCollection<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
    }
}