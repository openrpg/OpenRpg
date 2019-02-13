using System.Collections.Generic;
using OpenRpg.Core.Defaults;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Stats;
using OpenRpg.Genres.Fantasy.Stats;

namespace OpenRpg.Genres.Fantasy.Defaults
{
    public class DefaultStatsComputer : IStatsComputer
    {
        public IAttributeStatPopulator AttributeStatPopulator { get; }
        public IVitalStatsPopulator VitalStatsPopulator { get; }
        public IDamageStatPopulator DamageStatPopulator { get; }
        public IDefenseStatPopulator DefenseStatPopulator { get; }

        public DefaultStatsComputer(IAttributeStatPopulator attributeStatPopulator, IVitalStatsPopulator vitalStatsPopulator, IDamageStatPopulator damageStatPopulator, IDefenseStatPopulator defenseStatPopulator)
        {
            AttributeStatPopulator = attributeStatPopulator;
            VitalStatsPopulator = vitalStatsPopulator;
            DamageStatPopulator = damageStatPopulator;
            DefenseStatPopulator = defenseStatPopulator;
        }

        public IEntityStats ComputeStats(ICustomStatData customStatData, IReadOnlyCollection<Effect> effects)
        {
            var stats = new DefaultEntityStats();
            AttributeStatPopulator.PopulateStats(stats, customStatData, effects);
            DefenseStatPopulator.PopulateStats(stats, customStatData, effects);
            DamageStatPopulator.PopulateStats(stats, customStatData, effects);
            VitalStatsPopulator.PopulateStats(stats, customStatData, effects);
            return stats;
        }
    }
}