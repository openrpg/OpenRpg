using System.Collections.Generic;

namespace OpenRpg.Combat.Attacks
{
    public class ProcessedAttack
    {
        public ICollection<Damage> DamageDone { get; }
        public ICollection<Damage> DamageDefended { get; }

        public ProcessedAttack(ICollection<Damage> damageDone, ICollection<Damage> damageDefended)
        {
            DamageDone = damageDone;
            DamageDefended = damageDefended;
        }
    }
}