using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Effects;

namespace OpenRpg.Core.Variables.Populators
{
    public class CompositeVariablePopulator<T> : IVariablePopulator<T> where T : IVariables
    {
        public IEnumerable<IPartialVariablePopulator<T>> PartialPopulators { get; }

        public CompositeVariablePopulator(IEnumerable<IPartialVariablePopulator<T>> partialPopulators)
        { PartialPopulators = partialPopulators; }
        
        public void Populate(T vars, IReadOnlyCollection<Effect> activeEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            foreach (var populator in PartialPopulators.OrderByDescending(x => x.Priority))
            { populator.Populate(vars, activeEffects, relatedVars); }
        }
    }
}