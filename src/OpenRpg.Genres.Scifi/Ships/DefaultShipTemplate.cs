using System;
using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Genres.Scifi.Ships
{
    public class DefaultShipTemplate : IShipTemplate
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public IEnumerable<Effect> Effects { get; set; } = Array.Empty<Effect>();
        public IEnumerable<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
    }
}