using OpenRpg.Core.Stats;
using OpenRpg.Core.Variables.Populators;

namespace OpenRpg.Core.State.Populators
{
    /// <summary>
    /// Acts as a smaller part to be used in conjunction with other populators
    /// </summary>
    public interface IPartialStatePopulator<in T> : IPartialVariablePopulator<T> where T : IStateVariables
    {
    }
}