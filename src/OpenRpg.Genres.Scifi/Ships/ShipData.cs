using OpenRpg.Core.Templates;
using OpenRpg.Entities.State;
using OpenRpg.Entities.Stats;
using OpenRpg.Genres.Scifi.Variables;

namespace OpenRpg.Genres.Scifi.Ships
{
    public class ShipData : ITemplateData<ShipVariables>, IHasState<ShipStateVariables>, IHasStats<ShipStatsVariables>
    {
        public int TemplateId { get; set; }
        
        public ShipStatsVariables Stats { get; set; } = new();
        public ShipStateVariables State { get; set; } = new();
        public ShipVariables Variables { get; set; } = new();
    }
}