using System.Collections.Generic;
using OpenRpg.Entities.Entity.Populators.Stats;
using OpenRpg.Entities.Stats.Populators;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Fantasy.Stats.Populators.Partial;
using OpenRpg.Genres.Populators.Entity.Stats;

namespace OpenRpg.Genres.Fantasy.Stats.Populators
{
    public class FantasyStatsPopulator : CompositeStatPopulator<EntityStatsVariables>, IEntityStatPopulator
    {
        public FantasyStatsPopulator(IEnumerable<IEntityPartialStatPopulator> additionalStatPopulators = null)
        {
            var fantasyPopulators = new List<IEntityPartialStatPopulator>()
            {
                new FantasyAttributeStatPopulator(),
                new FantasyVitalsStatPopulator(),
                new FantasyMeleeStatPopulator(),
                new FantasyElementalStatPopulator(),
                new AbilityStatPopulator(),
                new CriticalStatPopulator()
            };

            if (additionalStatPopulators != null)
            { fantasyPopulators.AddRange(additionalStatPopulators); }

            PartialPopulators = fantasyPopulators.ToArray();
        }
    }
}