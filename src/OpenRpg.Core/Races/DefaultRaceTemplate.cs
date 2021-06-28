using System;
using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;

namespace OpenRpg.Core.Races
{
    public class DefaultRaceTemplate : IRaceTemplate
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        public IEnumerable<Effect> Effects { get; set; } = Array.Empty<Effect>();
        public IEnumerable<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
        public IRaceTemplateVariables Variables { get; set; } = new DefaultRaceTemplateVariables();
    }
}