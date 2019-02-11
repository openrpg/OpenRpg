using System.Collections.Generic;

namespace OpenRpg.Combat.Attacks
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