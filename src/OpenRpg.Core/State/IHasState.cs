using OpenRpg.Core.Stats.Variables;

namespace OpenRpg.Core.State
{
    public interface IHasState<out T> where T : IStateVariables
    {
        T State { get; }
    }
}