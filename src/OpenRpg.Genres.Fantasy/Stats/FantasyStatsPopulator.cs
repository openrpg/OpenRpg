using System.Collections.Generic;
using OpenRpg.Entities.Entity.Populators.Stats;
using OpenRpg.Entities.Stats.Populators;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Fantasy.Stats.Populators;

namespace OpenRpg.Genres.Fantasy.Stats
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
            };

            if (additionalStatPopulators != null)
            { fantasyPopulators.AddRange(additionalStatPopulators); }

            PartialPopulators = fantasyPopulators.ToArray();
        }
    }
}