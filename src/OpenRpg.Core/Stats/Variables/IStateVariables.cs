using OpenRpg.Core.Variables;

namespace OpenRpg.Core.Stats.Variables
{
    /// <summary>
    /// State variables are manually maintained state values which may depend upon Stat values but are tracked separately
    /// </summary>
    /// <remarks>
    /// Examples of state would be Health, CurrentAmmo, LivesRemaining, CurrentCash
    ///
    /// These values would often be serialized with game saves so you know what your characters active state was at
    /// a given time, this differs from the Stats which are more static data derived from computed information, whereas
    /// this is all manually maintained information.
    /// </remarks>
    public interface IStateVariables : IVariables<float>
    {}
}