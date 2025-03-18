using System.Collections.Generic;
using OpenRpg.Entities.State.Populators.Variables;

namespace OpenRpg.Entities.Stats.Populators
{
    public class CompositeStatPopulator<T> : CompositeVariablePopulator<T>, IStatPopulator<T> where T : IStatsVariables
    {
        public CompositeStatPopulator(IEnumerable<IPartialStatPopulator<T>> partialPopulators) : base(partialPopulators)
        {
        }

        protected CompositeStatPopulator()
        {
        }
    }
}