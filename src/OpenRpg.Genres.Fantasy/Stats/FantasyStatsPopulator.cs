using System.Collections.Generic;
using OpenRpg.Entities.Stats.Populators;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Fantasy.Stats.Populators;
using OpenRpg.Genres.Populators.Entity.Stats;

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
                new FantasyMeleeDamageStatPopulator(),
                new FantasyElementalDamageStatPopulator(),
                new FantasyMeleeDefenseStatPopulator(),
                new FantasyElementalDefenseStatPopulator()
            };

            if (additionalStatPopulators != null)
            { fantasyPopulators.AddRange(additionalStatPopulators); }

            PartialPopulators = fantasyPopulators.ToArray();
        }
    }
}