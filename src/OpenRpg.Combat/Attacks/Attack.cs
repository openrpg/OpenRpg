using System.Collections.Generic;

namespace OpenRpg.Combat.Attacks
{
    public class Attack
    {
        public ICollection<Damage> Damages { get; } 

        public Attack(ICollection<Damage> damages)
        {
            Damages = damages;
        }
    }
}