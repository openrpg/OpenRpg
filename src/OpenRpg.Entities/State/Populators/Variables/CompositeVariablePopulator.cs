using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Variables;
using OpenRpg.Entities.Effects;

namespace OpenRpg.Entities.State.Populators.Variables
{
    public class CompositeVariablePopulator<T> : IVariablePopulator<T> where T : IVariables
    {
        public IEnumerable<IPartialVariablePopulator<T>> PartialPopulators { get; protected set; }

        public CompositeVariablePopulator(IEnumerable<IPartialVariablePopulator<T>> partialPopulators)
        { PartialPopulators = partialPopulators; }

        protected CompositeVariablePopulator()
        {}

        public void Populate(T vars, IReadOnlyCollection<StaticEffect> activeEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            foreach (var populator in PartialPopulators.OrderByDescending(x => x.Priority))
            { populator.Populate(vars, activeEffects, relatedVars); }
        }
    }
}