using OpenRpg.Core.State.Populators.Variables;

namespace OpenRpg.Core.State.Populators
{
    /// <summary>
    /// Provides a mechanism to populate stats from active effects
    /// </summary>
    public interface IStatePopulator<in T> : IVariablePopulator<T> where T : IStateVariables
    {

    }
}