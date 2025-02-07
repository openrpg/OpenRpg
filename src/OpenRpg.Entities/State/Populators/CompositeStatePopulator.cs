using System.Collections.Generic;
using OpenRpg.Entities.State.Populators.Variables;

namespace OpenRpg.Entities.State.Populators
{
    public class CompositeStatePopulator<T> : CompositeVariablePopulator<T>, IStatePopulator<T> where T : IStateVariables
    {
        public CompositeStatePopulator(IEnumerable<IPartialStatePopulator<T>> partialPopulators) : base(partialPopulators)
        {
        }

        protected CompositeStatePopulator()
        {
        }
    }
}