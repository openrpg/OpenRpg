using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Effects.Processors;
using OpenRpg.Entities.Entity.Populators.Stats;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Types;

namespace OpenRpg.Genres.Populators.Entity.Stats
{
    public class DamageStatPopulator : IEntityPartialStatPopulator
    {
        public int Priority => 10;
        
        public void Populate(EntityStatsVariables stats, ComputedEffects computedEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            stats.Damage(computedEffects.CalculateTotalValueFor(GenreEffectTypes.DamageBonusAmount, 
                GenreEffectTypes.DamageBonusPercentage));
            stats.CriticalDamageChance(computedEffects.CalculateTotalValueFor(GenreEffectTypes.CriticalRateBonusAmount,
                GenreEffectTypes.CriticalRateBonusPercentage));
            stats.CriticalDamageMultiplier(computedEffects.CalculateTotalValueFor(GenreEffectTypes.CriticalDamageBonusAmount,
                GenreEffectTypes.CriticalDamageBonusPercentage));
        }
    }
}