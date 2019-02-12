using System.Collections.Generic;
using OpenRpg.Combat.Attacks;

namespace OpenRpg.Combat.Processors
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