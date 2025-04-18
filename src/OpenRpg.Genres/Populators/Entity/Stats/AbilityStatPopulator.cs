using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Entities.Effects.Processors;
using OpenRpg.Entities.Entity.Populators.Stats;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Types;

namespace OpenRpg.Genres.Populators.Entity.Stats
{
    public class AbilityStatPopulator : IEntityPartialStatPopulator
    {
        public int Priority => 10;

        public void Populate(EntityStatsVariables stats, ComputedEffects computedEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            stats.AttackRange(computedEffects.CalculateTotalValueFor(GenreEffectTypes.AttackRangeBonusAmount,
                GenreEffectTypes.AttackRangeBonusPercentage));
            
            stats.AttackSize(computedEffects.CalculateTotalValueFor(GenreEffectTypes.AttackRangeBonusAmount,
                GenreEffectTypes.AttackRangeBonusPercentage));
            
            stats.CooldownReduction(computedEffects.CalculateTotalValueFor(GenreEffectTypes.AttackRangeBonusAmount,
                GenreEffectTypes.AttackRangeBonusPercentage));
        }
    }
}