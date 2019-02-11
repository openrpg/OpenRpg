using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Core.Defaults.Conventions
{
    public class ConventionalRaceTemplate : IConventionalRaceTemplate
    {
        public int Id { get; set; }
        public string AssetCode { get; set; }
        public string NameLocaleId => $"{AssetCode}-name";
        public string DescriptionLocaleId => $"{AssetCode}-description";
        
        public IEnumerable<Requirement> Requirements { get; set; } = new List<Requirement>();
        public IEnumerable<Effect> Effects { get; set; } = new List<Effect>();
    }
}