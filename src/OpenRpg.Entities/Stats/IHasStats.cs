namespace OpenRpg.Entities.Stats
{
    public interface IHasStats<out T> where T : IStatsVariables
    {
        T Stats { get; }
    }
}