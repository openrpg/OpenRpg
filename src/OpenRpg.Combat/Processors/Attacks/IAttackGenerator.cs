using OpenRpg.Combat.Abilities;
using OpenRpg.Combat.Attacks;
using OpenRpg.Entities.Stats;

namespace OpenRpg.Combat.Processors.Attacks
{
    public interface IAttackGenerator<in T> where T : IStatsVariables
    {
        Attack GenerateAttack(T stats);
        Attack GenerateAttack(Ability ability, T stats);
        Attack GenerateAttack(Damage damage, T stats);
    }
}