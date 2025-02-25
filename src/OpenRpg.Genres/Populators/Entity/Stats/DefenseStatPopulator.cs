using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Effects.Processors;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Types;

namespace OpenRpg.Genres.Populators.Entity.Stats
{
    public class DefenseStatPopulator : IEntityPartialStatPopulator
    {
        public int Priority => 10;

        public float ComputeTotal(ComputedEffects computedEffects)
        {
            var amount = computedEffects.Get(GenreEffectTypes.DefenseBonusAmount);
            var percentage = computedEffects.Get(GenreEffectTypes.DefenseBonusPercentage);

            if (percentage == 0)
            { return amount; }

            var addition = amount * (percentage/100);
            return amount + addition;
        }
        
        public void Populate(EntityStatsVariables stats, ComputedEffects computedEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            var totalValue = ComputeTotal(computedEffects);
            stats.Defense(totalValue);
        }
    }
}