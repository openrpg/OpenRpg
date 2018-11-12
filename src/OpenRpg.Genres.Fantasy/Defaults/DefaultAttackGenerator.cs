using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Utils;
using OpenRpg.Genres.Fantasy.Combat;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Stats.Damage;

namespace OpenRpg.Genres.Fantasy.Defaults
{
    public class DefaultAttackGenerator : IAttackGenerator
    {
        public IRandomizer Randomizer { get; }

        public DefaultAttackGenerator(IRandomizer randomizer)
        {
            Randomizer = randomizer;
        }

        public float GenerateRandomFrom(float maximumValue, float startFrom = 0.75f)
        { return Randomizer.Random(maximumValue * startFrom, maximumValue); }

        public Attack GenerateAttack(IDamageStats damageStats)
        {
            var damages = new List<Damage>();
            var applicableDamages = damageStats.AsList().Where(x => x.StatValue != 0);

            foreach (var applicableDamage in applicableDamages)
            {
                var actualDamage = GenerateRandomFrom(applicableDamage.StatValue);
                var newDamage = new Damage(applicableDamage.StatType, actualDamage);
                damages.Add(newDamage);
            }

            return new Attack(damages);
        }
    }
}