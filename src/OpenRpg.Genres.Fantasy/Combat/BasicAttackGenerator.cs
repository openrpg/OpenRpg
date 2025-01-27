using System.Linq;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Processors.Attacks.Entity;
using OpenRpg.Core.Stats.Variables;
using OpenRpg.Genres.Fantasy.Extensions;

namespace OpenRpg.Genres.Fantasy.Combat
{
    public class BasicAttackGenerator : IEntityAttackGenerator
    {
        public Attack GenerateAttack(EntityStatsVariables stats)
        {
            var damages = (stats as EntityStatsVariables)?.GetDamageReferences()
                .Where(x => x.StatValue != 0)
                .Select(x => new Damage(x.StatType, x.StatValue))
                .ToArray();

            return new Attack(damages);
        }
    }
}