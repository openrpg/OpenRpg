using System.Collections.Generic;
using OpenRpg.Core.State.Populators.Variables;

namespace OpenRpg.Core.Stats.Populators
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