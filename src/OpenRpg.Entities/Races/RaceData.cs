using OpenRpg.Core.Templates;
using OpenRpg.Entities.Races.Variables;

namespace OpenRpg.Entities.Races
{
    public class RaceData : ITemplateData<RaceVariables>
    {
        public int TemplateId { get; set; }
        public RaceVariables Variables { get; set; } = new RaceVariables();
    }
}