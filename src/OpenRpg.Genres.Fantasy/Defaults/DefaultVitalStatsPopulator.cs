using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Stats;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Stats;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Defaults
{
    public class DefaultVitalStatsPopulator : IVitalStatsPopulator
    {
        public void PopulateStats(IEntityStats stats, ICustomStatData customStatData, IReadOnlyCollection<Effect> activeEffects)
        {
            var constitutionBonus = stats.Constitution() * 5;
            var effectBonus = activeEffects.GetPotencyFor(EffectTypes.HealthBonusAmount);
            var effectBonusPercentage = activeEffects.GetPotencyFor(EffectTypes.HealthBonusPercentage);
            var maxHealthStat = constitutionBonus + effectBonus;
            var additionalHealth = maxHealthStat * effectBonusPercentage;
            maxHealthStat += additionalHealth;

            stats.Health(maxHealthStat);
            stats.MaxHealth(maxHealthStat);
        }
    }
}