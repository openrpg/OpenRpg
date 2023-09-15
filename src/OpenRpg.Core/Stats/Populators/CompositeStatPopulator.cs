using System.Collections.Generic;
using OpenRpg.Core.Variables.Populators;

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