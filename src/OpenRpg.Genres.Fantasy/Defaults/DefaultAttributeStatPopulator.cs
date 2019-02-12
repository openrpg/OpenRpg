using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Stats;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Stats;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Defaults
{
    public class DefaultAttributeStatPopulator : IAttributeStatPopulator
    {
        public void PopulateStats(IEntityStats stats, ICustomStatData customStatData, IReadOnlyCollection<Effect> activeEffects)
        {
            var strengthValue = activeEffects.CalculateBonusFor(EffectTypes.StrengthBonusAmount, EffectTypes.StrengthBonusPercentage);
            var dexterityValue = activeEffects.CalculateBonusFor(EffectTypes.DexterityBonusAmount, EffectTypes.DexterityBonusPercentage);
            var constitutionValue = activeEffects.CalculateBonusFor(EffectTypes.ConstitutionBonusAmount, EffectTypes.ConstitutionBonusPercentage);
            var intelligenceValue = activeEffects.CalculateBonusFor(EffectTypes.IntelligenceBonusAmount, EffectTypes.IntelligenceBonusPercentage);
            var wisdomValue = activeEffects.CalculateBonusFor(EffectTypes.WisdomBonusAmount, EffectTypes.WisdomBonusPercentage);
            var charismaValue = activeEffects.CalculateBonusFor(EffectTypes.CharismaBonusAmount, EffectTypes.CharismaBonusPercentage);

            stats.Strength(strengthValue);
            stats.Dexterity(dexterityValue);
            stats.Constitution(constitutionValue);
            stats.Intelligence(intelligenceValue);
            stats.Wisdom(wisdomValue);
            stats.Charisma(charismaValue);
        }
    }
}