using OpenRpg.Core.State.Populators.Variables;
using OpenRpg.Core.Stats;

namespace OpenRpg.Core.State.Populators
{
    /// <summary>
    /// Acts as a smaller part to be used in conjunction with other populators
    /// </summary>
    public interface IPartialStatePopulator<in T> : IPartialVariablePopulator<T> where T : IStateVariables
    {
    }
}