using OpenRpg.Combat.Attacks;
using OpenRpg.Core.Stats;

namespace OpenRpg.Combat.Processors
{
    public interface IAttackProcessor
    {
        ProcessedAttack ProcessAttack(Attack attack, IStatsVariables stats);
    }
}