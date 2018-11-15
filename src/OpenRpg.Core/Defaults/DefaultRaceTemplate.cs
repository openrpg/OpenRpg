using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Races;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Core.Defaults
{
    public class DefaultRaceTemplate : IRaceTemplate
    {
        public int Id { get; set; }
        public string AssetCode { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public IEnumerable<Effect> Effects { get; set; }
        public IEnumerable<Requirement> Requirements { get; set; }
    }
}