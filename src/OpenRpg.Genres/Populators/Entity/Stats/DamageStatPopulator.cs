using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Types;

namespace OpenRpg.Genres.Populators.Entity.Stats
{
    public class DamageStatPopulator : IEntityPartialStatPopulator
    {
        public int Priority => 10;

        public float ComputeTotal(IReadOnlyCollection<StaticEffect> effects)
        {
            var amount = effects.GetPotencyFor(GenreEffectTypes.DamageBonusAmount);
            var percentage = effects.GetPotencyFor(GenreEffectTypes.DamageBonusPercentage);

            if (percentage == 0)
            { return amount; }

            var addition = amount * (percentage/100);
            return amount + addition;
        }
        
        public void Populate(EntityStatsVariables stats, IReadOnlyCollection<StaticEffect> activeEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            var totalValue = ComputeTotal(activeEffects);
            stats.Damage(totalValue);
        }
    }
}