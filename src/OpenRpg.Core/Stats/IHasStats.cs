namespace OpenRpg.Core.Stats
{
    public interface IHasStats<out T> where T : IStatsVariables
    {
        T Stats { get; }
    }
}