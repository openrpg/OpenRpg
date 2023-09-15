using System.Collections.Generic;
using OpenRpg.Core.Stats;
using OpenRpg.Core.Stats.Populators;
using OpenRpg.Core.Variables.Populators;

namespace OpenRpg.Core.State.Populators
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