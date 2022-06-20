using OpenRpg.Combat.Attacks;
using OpenRpg.Core.Stats;
using OpenRpg.Core.Stats.Variables;

namespace OpenRpg.Combat.Processors
{
    public interface IAttackProcessor
    {
        ProcessedAttack ProcessAttack(Attack attack, IStatsVariables stats);
    }
}