using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Entities.Effects.Processors;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Stats.Variables;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Genres.Populators.Entity.Stats;

namespace OpenRpg.Genres.Fantasy.Stats.Populators
{
    public class FantasyAttributeStatPopulator : IEntityPartialStatPopulator
    {
        public int Priority => 100;

        public void Populate(EntityStatsVariables stats, ComputedEffects computedEffects, IReadOnlyCollection<IVariables> relatedVars)
        {
            stats.Strength((int)computedEffects.CalculateTotalValueFor(FantasyEffectTypes.StrengthBonusAmount, FantasyEffectTypes.StrengthBonusPercentage));
            stats.Dexterity((int)computedEffects.CalculateTotalValueFor(FantasyEffectTypes.DexterityBonusAmount, FantasyEffectTypes.DexterityBonusPercentage));
            stats.Constitution((int)computedEffects.CalculateTotalValueFor(FantasyEffectTypes.ConstitutionBonusAmount, FantasyEffectTypes.ConstitutionBonusPercentage));
            stats.Intelligence((int)computedEffects.CalculateTotalValueFor(FantasyEffectTypes.IntelligenceBonusAmount, FantasyEffectTypes.IntelligenceBonusPercentage));
            stats.Wisdom((int)computedEffects.CalculateTotalValueFor(FantasyEffectTypes.WisdomBonusAmount, FantasyEffectTypes.WisdomBonusPercentage));
            stats.Charisma((int)computedEffects.CalculateTotalValueFor(FantasyEffectTypes.CharismaBonusAmount, FantasyEffectTypes.CharismaBonusPercentage));
        }
    }
}