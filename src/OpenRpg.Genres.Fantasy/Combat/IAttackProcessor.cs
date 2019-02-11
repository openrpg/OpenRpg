using OpenRpg.Combat.Attacks;
using OpenRpg.Genres.Fantasy.Stats.Defense;

namespace OpenRpg.Genres.Fantasy.Combat
{
    public interface IAttackProcessor
    {
        ProcessedAttack ProcessAttack(Attack attack, IDefenseStats defenseStats);
    }
}