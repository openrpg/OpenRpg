using System.Collections.Generic;
using OpenRpg.Core.Stats.Entity;
using OpenRpg.Core.Stats.Populators;
using OpenRpg.Genres.Fantasy.Stats.Populators;
using OpenRpg.Genres.Populators.Entity.Stats;

namespace OpenRpg.Genres.Fantasy.Stats
{
    public class FantasyStatsPopulator : CompositeStatPopulator<IEntityStatsVariables>, IEntityStatPopulator
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