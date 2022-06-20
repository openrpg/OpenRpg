using OpenRpg.Combat.Attacks;
using OpenRpg.Core.Stats;
using OpenRpg.Core.Stats.Variables;

namespace OpenRpg.Combat.Processors
{
    public interface IAttackGenerator
    {
        Attack GenerateAttack(IStatsVariables stats);
    }
}