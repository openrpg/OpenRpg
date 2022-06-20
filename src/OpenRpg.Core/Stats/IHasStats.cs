using OpenRpg.Core.Stats.Variables;

namespace OpenRpg.Core.Stats
{
    public interface IHasStats
    {
        IStatsVariables Stats { get; }
    }
}