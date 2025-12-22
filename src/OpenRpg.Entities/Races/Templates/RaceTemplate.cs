using OpenRpg.Core.Templates;
using OpenRpg.Entities.Races.Variables;

namespace OpenRpg.Entities.Races.Templates
{
    public class RaceTemplate : ITemplate<RaceTemplateVariables>
    {
        public int Id { get; set; }
        public string NameLocaleId { get; set; }
        public string DescriptionLocaleId { get; set; }
        
        public RaceTemplateVariables Variables { get; set; } = new RaceTemplateVariables();
    }
}