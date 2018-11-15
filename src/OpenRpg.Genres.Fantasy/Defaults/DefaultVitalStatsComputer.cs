using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Genres.Fantasy.Stats.Attributes;
using OpenRpg.Genres.Fantasy.Stats.Vitals;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Defaults
{
    public class DefaultVitalStatsComputer : IVitalStatsComputer
    {
        public IVitalStats ComputeStats(IVitalStats baseVitalStats, IAttributeStats baseAttributeStats, ICollection<Effect> effects)
        {
            var constitutionBonus = baseAttributeStats.Constitution * 5;
            var effectBonus = effects.GetPotencyFor(EffectTypes.HealthBonusAmount);
            var maxHealthStat = baseVitalStats.MaxHealth + constitutionBonus + effectBonus;

            return new VitalStats
            {
                MaxHealth = maxHealthStat
            };
        }
    }
}