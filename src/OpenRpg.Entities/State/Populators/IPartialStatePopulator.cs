using OpenRpg.Entities.State.Populators.Variables;

namespace OpenRpg.Entities.State.Populators
{
    /// <summary>
    /// Acts as a smaller part to be used in conjunction with other populators
    /// </summary>
    public interface IPartialStatePopulator<in T> : IPartialVariablePopulator<T> where T : IStateVariables
    {
    }
}