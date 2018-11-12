using System.Collections.Generic;

namespace OpenRpg.Genres.Fantasy.Combat
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