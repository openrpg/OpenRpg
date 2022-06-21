using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Stats.Variables;

namespace OpenRpg.Core.Variables
{
    public interface IVariablePopulator<T> where T : IVariables
    {
        T Populate(IReadOnlyCollection<Effect> activeEffects, IReadOnlyCollection<IVariables> variables);
        void Repopulate(T existingVars, IReadOnlyCollection<Effect> activeEffects, IReadOnlyCollection<IVariables> variables);
    }
}