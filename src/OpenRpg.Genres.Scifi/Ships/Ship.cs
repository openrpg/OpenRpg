using OpenRpg.Core.Templates;
using OpenRpg.Core.Variables.General;
using OpenRpg.Entities.State;
using OpenRpg.Entities.Stats;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Scifi.Variables;

namespace OpenRpg.Genres.Scifi.Ships
{
    public class Ship : IHasTemplate<ShipTemplate>, IHasState<ShipStateVariables>, IHasStats<ShipStatsVariables>, IHasVariables<ShipVariables>
    {
        public Character Pilot { get; set; }
        public ShipTemplate Template { get; set; } = new ShipTemplate();
        public ShipStatsVariables Stats { get; set; } = new ShipStatsVariables();
        public ShipStateVariables State { get; set; } = new ShipStateVariables();
        public ShipVariables Variables { get; set; } = new ShipVariables();
    }
}