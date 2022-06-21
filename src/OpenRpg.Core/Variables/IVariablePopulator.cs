using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Stats.Variables;

namespace OpenRpg.Core.Variables
{
    public interface IVariablePopulator<T> where T : IVariables
    {
        void Populate(T varsToPopulate, IReadOnlyCollection<Effect> activeEffects, IReadOnlyCollection<IVariables> variables);
    }
}