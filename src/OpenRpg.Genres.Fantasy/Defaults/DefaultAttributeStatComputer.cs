using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Genres.Fantasy.Stats.Attributes;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Genres.Fantasy.Defaults
{
    public class DefaultAttributeStatComputer : IAttributeStatComputer
    {
        public IAttributeStats ComputeStats(IAttributeStats baseAttributeStats, ICollection<Effect> activeEffects)
        {
            var strengthAugmentation = activeEffects.GetPotencyFor(BonusTypes.StrengthBonus);
            var dexterityAugmentation = activeEffects.GetPotencyFor(BonusTypes.DexterityBonus);
            var constitutionAugmentation = activeEffects.GetPotencyFor(BonusTypes.ConstitutionBonus);
            var intelligenceAugmentation = activeEffects.GetPotencyFor(BonusTypes.IntelligenceBonus);
            var wisdomAugmentation = activeEffects.GetPotencyFor(BonusTypes.WisdomBonus);
            var charismaAugmentation = activeEffects.GetPotencyFor(BonusTypes.CharismaBonus);

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