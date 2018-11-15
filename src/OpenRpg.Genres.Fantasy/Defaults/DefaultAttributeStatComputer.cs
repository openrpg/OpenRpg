using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Genres.Fantasy.Stats.Attributes;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Defaults
{
    public class DefaultAttributeStatComputer : IAttributeStatComputer
    {
        public int CalculateBonusFor(int baseStat, int amountBonusType, int percentageBonusType, ICollection<Effect> activeEffects)
        {
            var percentageBonus = activeEffects.GetPotencyFor(percentageBonusType);
            if(percentageBonus != 0) { percentageBonus = percentageBonusType / 100; }

            var amountAugmentation = baseStat * percentageBonus;
            return activeEffects.GetPotencyFor(amountBonusType) + amountAugmentation;
        }
        
        public IAttributeStats ComputeStats(IAttributeStats baseAttributeStats, ICollection<Effect> activeEffects)
        {           
            var strengthAugmentation = CalculateBonusFor(baseAttributeStats.Strength, EffectTypes.StrengthBonusAmount, EffectTypes.StrengthBonusPercentage, activeEffects);
            var dexterityAugmentation = CalculateBonusFor(baseAttributeStats.Strength, EffectTypes.DexterityBonusAmount, EffectTypes.DexterityBonusPercentage, activeEffects);
            var constitutionAugmentation = CalculateBonusFor(baseAttributeStats.Strength, EffectTypes.ConstitutionBonusAmount, EffectTypes.ConstitutionBonusPercentage, activeEffects);
            var intelligenceAugmentation = CalculateBonusFor(baseAttributeStats.Strength, EffectTypes.IntelligenceBonusAmount, EffectTypes.IntelligenceBonusPercentage, activeEffects);
            var wisdomAugmentation = CalculateBonusFor(baseAttributeStats.Strength, EffectTypes.WisdomBonusAmount, EffectTypes.WisdomBonusPercentage, activeEffects);
            var charismaAugmentation = CalculateBonusFor(baseAttributeStats.Strength, EffectTypes.CharismaBonusAmount, EffectTypes.CharismaBonusPercentage, activeEffects);

            return new AttributeStats
            {
                Strength = (byte) (baseAttributeStats.Strength + strengthAugmentation),
                Dexterity = (byte) (baseAttributeStats.Dexterity + dexterityAugmentation),
                Constitution = (byte) (baseAttributeStats.Constitution + constitutionAugmentation),
                Intelligence = (byte) (baseAttributeStats.Intelligence + intelligenceAugmentation),
                Wisdom = (byte) (baseAttributeStats.Wisdom + wisdomAugmentation),
                Charisma = (byte) (baseAttributeStats.Charisma + charismaAugmentation)
            };
        }
    }
}