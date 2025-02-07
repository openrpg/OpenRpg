using System;
using System.Collections.Generic;
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
        public IReadOnlyCollection<StaticEffect> Effects { get; set; } = Array.Empty<StaticEffect>();
        public IReadOnlyCollection<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
    }
}