using System.Collections.Generic;
using OpenRpg.Core.Effects;

namespace OpenRpg.Core.Variables.Computed
{
    public interface IComputedVariablePopulator<out T> where T : IComputedVariables
    {
        T Populate(IReadOnlyCollection<IVariables> variables, IReadOnlyCollection<Effect> activeEffects);
    }
}