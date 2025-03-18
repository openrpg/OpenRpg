using OpenRpg.Entities.State.Populators.Variables;

namespace OpenRpg.Entities.Stats.Populators
{
    /// <summary>
    /// Acts as a smaller part to be used in conjunction with other populators
    /// </summary>
    public interface IPartialStatPopulator<in T> : IPartialVariablePopulator<T> where T : IStatsVariables
    {
    }
}