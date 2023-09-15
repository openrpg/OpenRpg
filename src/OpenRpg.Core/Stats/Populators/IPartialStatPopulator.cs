using OpenRpg.Core.Variables.Populators;

namespace OpenRpg.Core.Stats.Populators
{
    /// <summary>
    /// Acts as a smaller part to be used in conjunction with other populators
    /// </summary>
    public interface IPartialStatPopulator<in T> : IPartialVariablePopulator<T> where T : IStatsVariables
    {
    }
}