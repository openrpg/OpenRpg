using System;
using System.Collections.Generic;

namespace OpenRpg.Combat.Attacks
{
    public class Attack
    {
        public bool IsCritical { get; set; }
        public IReadOnlyList<Damage> Damages { get; set; } = Array.Empty<Damage>(); 

        public Attack(){}
        public Attack(IReadOnlyList<Damage> damages, bool isCritical = false)
        {
            Damages = damages;
            IsCritical = isCritical;
        }
    }
}