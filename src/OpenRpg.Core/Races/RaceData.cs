using OpenRpg.Core.Races.Variables;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Races
{
    public class RaceData : ITemplateData<RaceVariables>
    {
        public int TemplateId { get; set; }
        public RaceVariables Variables { get; set; } = new RaceVariables();
    }
}