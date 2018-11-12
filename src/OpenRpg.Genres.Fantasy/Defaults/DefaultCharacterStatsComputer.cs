using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Genres.Fantasy.Stats;
using OpenRpg.Genres.Fantasy.Stats.Attributes;
using OpenRpg.Genres.Fantasy.Stats.Damage;
using OpenRpg.Genres.Fantasy.Stats.Defense;
using OpenRpg.Genres.Fantasy.Stats.Vitals;

namespace OpenRpg.Genres.Fantasy.Defaults
{
    public class DefaultCharacterStatsComputer : ICharacterStatsComputer
    {
        public IAttributeStatComputer AttributeStatComputer { get; }
        public IVitalStatsComputer VitalStatsComputer { get; }
        public IDamageStatComputer DamageStatComputer { get; }
        public IDefenseStatComputer DefenseStatComputer { get; }

        public DefaultCharacterStatsComputer(IAttributeStatComputer attributeStatComputer, IVitalStatsComputer vitalStatsComputer, IDamageStatComputer damageStatComputer, IDefenseStatComputer defenseStatComputer)
        {
            AttributeStatComputer = attributeStatComputer;
            VitalStatsComputer = vitalStatsComputer;
            DamageStatComputer = damageStatComputer;
            DefenseStatComputer = defenseStatComputer;
        }

        public ICharacterStats ComputeStats(IAttributeStats alteredStats, ICollection<Effect> effects)
        {
            var attributeStats = AttributeStatComputer.ComputeStats(alteredStats, effects);
            return new CharacterStats
            {
                AttributeStats = attributeStats,
                DefenseStats = DefenseStatComputer.ComputeStats(DefenseStats.Default, attributeStats, effects),
                DamageStats = DamageStatComputer.ComputeStats(DamageStats.Default, attributeStats, effects),
                VitalStats = VitalStatsComputer.ComputeStats(VitalStats.Default, attributeStats, effects)
            };
        }
    }
}