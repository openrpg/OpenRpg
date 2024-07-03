using OpenRpg.Combat.Attacks;
using OpenRpg.Core.Stats;

namespace OpenRpg.Combat.Processors.Attacks
{
    public interface IAttackGenerator<in T> where T : IStatsVariables
    {
        Attack GenerateAttack(T stats);
    }
}