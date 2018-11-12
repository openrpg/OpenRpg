using System.Collections.Generic;

namespace OpenRpg.Genres.Fantasy.Combat
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