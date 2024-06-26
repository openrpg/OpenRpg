using System.Linq;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Processors.Attacks;
using OpenRpg.Genres.Scifi.Extensions;
using OpenRpg.Genres.Scifi.Variables;

namespace OpenRpg.Genres.Scifi.Combat
{
    public class ShipAttackGenerator : IAttackGenerator<IShipStatsVariables>
    {
        public Attack GenerateAttack(IShipStatsVariables stats)
        {
            var damages = stats.GetDamageReferences()
                .Where(x => x.StatValue != 0)
                .Select(x => new Damage(x.StatType, x.StatValue))
                .ToArray();

            return new Attack(damages);
        }
    }
}