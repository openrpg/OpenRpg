using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Scifi.Variables;

namespace OpenRpg.Genres.Scifi.Ships
{
    public class DefaultShip : IShip
    {
        public ICharacter Pilot { get; set; }
        public IShipTemplate ShipTemplate { get; set; } = new DefaultShipTemplate();
        public IShipStatsVariables Stats { get; set; } = new DefaultShipStatsVariables();
        public IShipStateVariables State { get; set; } = new DefaultShipStateVariables();
        public IShipVariables Variables { get; set; } = new DefaultShipVariables();
    }
}