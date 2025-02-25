using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Variables;
using OpenRpg.Entities.Effects.Processors;

namespace OpenRpg.Entities.State.Populators.Variables
{
    public class CompositeVariablePopulator<T> : IVariablePopulator<T> where T : IVariables
    {
        public IEnumerable<IPartialVariablePopulator<T>> PartialPopulators { get; protected set; }

        public CompositeVariablePopulator(IEnumerable<IPartialVariablePopulator<T>> partialPopulators)
        { PartialPopulators = partialPopulators; }

        protected CompositeVariablePopulator()
        {}

        public void Populate(T vars, ComputedEffects computedEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            var orderedPopulators = PartialPopulators.OrderByDescending(x => x.Priority).ToArray();
            foreach (var populator in orderedPopulators)
            { populator.Populate(vars, computedEffects, relatedVars); }
        }
    }
}