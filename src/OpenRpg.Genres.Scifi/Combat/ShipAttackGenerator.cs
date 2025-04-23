using System.Linq;
using OpenRpg.Combat.Abilities;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Extensions;
using OpenRpg.Combat.Processors.Attacks;
using OpenRpg.Genres.Scifi.Extensions;
using OpenRpg.Genres.Scifi.Variables;

namespace OpenRpg.Genres.Scifi.Combat
{
    public class ShipAttackGenerator : IAttackGenerator<ShipStatsVariables>
    {
        public Attack GenerateAttack(ShipStatsVariables stats)
        {
            var damages = stats.GetDamageReferences()
                .Where(x => x.StatValue != 0)
                .Select(x => new Damage(x.StatType, x.StatValue))
                .ToArray();

            return new Attack(damages);
        }

        public Attack GenerateAttack(Ability ability, ShipStatsVariables stats)
        {
            var baseDamage = ability.Template.Variables.Damage();
            return GenerateAttack(baseDamage, stats);
        }

        public Attack GenerateAttack(Damage damage, ShipStatsVariables stats)
        {
            damage.Value += stats.GetDamageFor(damage.Type);
            return new Attack(new[] { damage });
        }
    }
}