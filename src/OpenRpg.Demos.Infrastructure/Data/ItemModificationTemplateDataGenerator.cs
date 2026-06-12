using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Modifications.Templates;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items.Templates;

namespace OpenRpg.Demos.Infrastructure.Data
{
    public class ItemModificationTemplateDataGenerator : IDataGenerator<ItemModificationTemplate>
    {
        public const int FireGemId = 1;
        public const int FrostGemId = 2;
        public const int PowerEnchantmentId = 3;
        public const int SpeedRuneId = 4;

        public IEnumerable<ItemModificationTemplate> GenerateData()
        {
            return new[]
            {
                MakeFireGem(),
                MakeFrostGem(),
                MakePowerEnchantment(),
                MakeSpeedRune()
            };
        }

        private ItemModificationTemplate MakeFireGem()
        {
            var template = new ItemModificationTemplate
            {
                Id = FireGemId,
                NameLocaleId = "Fire Gem",
                DescriptionLocaleId = "A glowing red gem that adds fire damage to weapons",
                ModificationType = FantasyModificationTypes.GemModification
            };
            template.Variables.Effects = new[]
            {
                new StaticEffect { EffectType = FantasyEffectTypes.FireDamageAmount, Potency = 15.0f }
            };
            return template;
        }

        private ItemModificationTemplate MakeFrostGem()
        {
            var template = new ItemModificationTemplate
            {
                Id = FrostGemId,
                NameLocaleId = "Frost Gem",
                DescriptionLocaleId = "A shimmering blue gem that adds ice damage to weapons",
                ModificationType = FantasyModificationTypes.GemModification
            };
            template.Variables.Effects = new[]
            {
                new StaticEffect { EffectType = FantasyEffectTypes.IceDamageAmount, Potency = 12.0f }
            };
            return template;
        }

        private ItemModificationTemplate MakePowerEnchantment()
        {
            var template = new ItemModificationTemplate
            {
                Id = PowerEnchantmentId,
                NameLocaleId = "Power Enchantment",
                DescriptionLocaleId = "A magical enchantment that boosts strength and raw damage",
                ModificationType = FantasyModificationTypes.EnchantmentModification
            };
            template.Variables.Effects = new[]
            {
                new StaticEffect { EffectType = FantasyEffectTypes.StrengthBonusAmount, Potency = 5.0f },
                new StaticEffect { EffectType = FantasyEffectTypes.DamageBonusAmount, Potency = 10.0f }
            };
            return template;
        }

        private ItemModificationTemplate MakeSpeedRune()
        {
            var template = new ItemModificationTemplate
            {
                Id = SpeedRuneId,
                NameLocaleId = "Speed Rune",
                DescriptionLocaleId = "An ancient rune that enhances dexterity and evasion",
                ModificationType = FantasyModificationTypes.RuneModification
            };
            template.Variables.Effects = new[]
            {
                new StaticEffect { EffectType = FantasyEffectTypes.DexterityBonusAmount, Potency = 8.0f }
            };
            return template;
        }
    }
}
