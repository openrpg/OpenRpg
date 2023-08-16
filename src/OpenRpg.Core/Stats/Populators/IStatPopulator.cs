using OpenRpg.Core.Stats.Variables;
using OpenRpg.Core.Variables.Populators;

namespace OpenRpg.Core.Stats.Populators
{
    /// <summary>
    /// Provides a mechanism to populate stats from active effects
    /// </summary>
    public interface IStatPopulator : IVariablePopulator<IStatsVariables>
    {

    }
}