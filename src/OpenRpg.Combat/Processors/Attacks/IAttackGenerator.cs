using OpenRpg.Combat.Attacks;
using OpenRpg.Core.Stats.Variables;

namespace OpenRpg.Combat.Processors.Attacks
{
    public interface IAttackGenerator
    {
        Attack GenerateAttack(IStatsVariables stats);
    }
}