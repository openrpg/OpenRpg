using OpenRpg.Combat.Attacks;
using OpenRpg.Entities.Stats;

namespace OpenRpg.Combat.Processors.Attacks
{
    public interface IAttackProcessor<in T> where T : IStatsVariables
    {
        ProcessedAttack ProcessAttack(Attack attack, T stats);
    }
}