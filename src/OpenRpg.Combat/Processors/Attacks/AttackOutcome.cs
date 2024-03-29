using System.Collections.Generic;
using OpenRpg.Combat.Attacks;

namespace OpenRpg.Combat.Processors.Attacks
{
    public class AttackOutcome
    {
        public ICollection<Damage> DamageDone { get; }
        public ICollection<Damage> DamageDefended { get; }

        public AttackOutcome(ICollection<Damage> damageDone, ICollection<Damage> damageDefended)
        {
            DamageDone = damageDone;
            DamageDefended = damageDefended;
        }
    }
}