using OpenRpg.Combat.Attacks;
using OpenRpg.Core.Stats;
using OpenRpg.Core.Stats.Entity;

namespace OpenRpg.Combat.Processors.Attacks
{
    public interface IAttackGenerator<in T> where T : IStatsVariables
    {
        Attack GenerateAttack(T stats);
    }
}