using System.Collections.Generic;
using OpenRpg.Core.Effects;

namespace OpenRpg.Core.Variables
{
    public interface IVariablePopulator<out T> where T : IVariables
    {
        T Populate(IReadOnlyCollection<Effect> activeEffects, IReadOnlyCollection<IVariables> variables);
    }
}