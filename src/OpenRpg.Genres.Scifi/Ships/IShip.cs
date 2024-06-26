using OpenRpg.Core.State;
using OpenRpg.Core.Stats;
using OpenRpg.Core.Variables.General;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Scifi.Variables;

namespace OpenRpg.Genres.Scifi.Ships
{
    public interface IShip : IHasState<IShipStateVariables>, IHasStats<IShipStatsVariables>, IHasVariables<IShipVariables>
    {
        ICharacter Pilot { get; }
        IShipTemplate ShipTemplate { get; }
    }
}