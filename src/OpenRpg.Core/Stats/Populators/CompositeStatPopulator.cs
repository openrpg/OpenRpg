using System.Collections.Generic;
using OpenRpg.Core.Stats.Variables;
using OpenRpg.Core.Variables.Populators;

namespace OpenRpg.Core.Stats.Populators
{
    public class CompositeStatPopulator : CompositeVariablePopulator<IStatsVariables>, IStatPopulator
    {
        public CompositeStatPopulator(IEnumerable<IPartialStatPopulator> partialPopulators) : base(partialPopulators)
        {
        }

        protected CompositeStatPopulator()
        {
        }
    }
}