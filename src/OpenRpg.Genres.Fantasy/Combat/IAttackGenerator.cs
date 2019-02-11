using OpenRpg.Combat.Attacks;
using OpenRpg.Genres.Fantasy.Stats.Damage;

namespace OpenRpg.Genres.Fantasy.Combat
{
    public interface IAttackGenerator
    {
        Attack GenerateAttack(IDamageStats damageStats);
    }
}