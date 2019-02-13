using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Processors;
using OpenRpg.Core.Stats;
using OpenRpg.Core.Utils;
using OpenRpg.Genres.Fantasy.Extensions;

namespace OpenRpg.Genres.Fantasy.Combat
{
    public class BasicAttackGenerator : IAttackGenerator
    {
        public IRandomizer Randomizer { get; }

        public BasicAttackGenerator(IRandomizer randomizer)
        {
            Randomizer = randomizer;
        }

        public float GenerateRandomFrom(float maximumValue, float startFrom = 0.75f)
        { return Randomizer.Random(maximumValue * startFrom, maximumValue); }

        public Attack GenerateAttack(IEntityStats stats)
        {
            var damages = new List<Damage>();
            var applicableDamages = stats.GetDamageReferences().Where(x => x.StatValue != 0);

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