using OpenRpg.Combat.Attacks;
using OpenRpg.Core.Stats;

namespace OpenRpg.Combat.Processors
{
    public interface IAttackGenerator
    {
        Attack GenerateAttack(IStatsVariables stats);
    }
}