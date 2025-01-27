using OpenRpg.Core.Variables;

namespace OpenRpg.Core.State.Populators.Variables
{
    /// <summary>
    /// A partial variable populator which should be used in conjunction with other paritials to work as a complete populator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPartialVariablePopulator<in T> : IVariablePopulator<T> where T : IVariables
    {
        /// <summary>
        /// The higher the priority the earlier its run
        /// </summary>
        int Priority { get; }
    }
}