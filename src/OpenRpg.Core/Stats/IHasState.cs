using OpenRpg.Core.Stats.Variables;

namespace OpenRpg.Core.Stats
{
    public interface IHasState
    {
        IStateVariables State { get; }
    }
}