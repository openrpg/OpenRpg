using OpenRpg.Entities.State.Populators.Variables;

namespace OpenRpg.Entities.Stats.Populators
{
    /// <summary>
    /// Provides a mechanism to populate stats from active effects
    /// </summary>
    public interface IStatPopulator<in T> : IVariablePopulator<T> where T : IStatsVariables
    {

    }
}