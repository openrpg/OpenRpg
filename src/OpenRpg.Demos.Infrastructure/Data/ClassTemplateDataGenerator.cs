using System.Collections.Generic;
using OpenRpg.Core.Classes.Templates;
using OpenRpg.Core.Effects;
using OpenRpg.Demos.Infrastructure.Extensions;
using OpenRpg.Demos.Infrastructure.Lookups;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Demos.Infrastructure.Data
{
    public class ClassTemplateDataGenerator : IDataGenerator<ClassTemplate>
    {
        public IEnumerable<ClassTemplate> GenerateData()
        {
            return new []
            {
                GenerateFighterClass(),
                GenerateMageClass()
            };
        }

        public ClassTemplate GenerateFighterClass()
        {
            var effects = new[]
            {
                new StaticEffect {Potency = 2, EffectType = FantasyEffectTypes.StrengthBonusAmount},
                new StaticEffect {Potency = 2, EffectType = FantasyEffectTypes.ConstitutionBonusAmount},
                new StaticEffect {Potency = 5, EffectType = FantasyEffectTypes.DamageBonusAmount},
                new StaticEffect {Potency = 5, EffectType = FantasyEffectTypes.DefenseBonusAmount},
                new StaticEffect {Potency = 30, EffectType = FantasyEffectTypes.HealthBonusAmount}
            };

            var classTemplate = new ClassTemplate
            {
                Id = ClassTypeLookups.Fighter,
                NameLocaleId = "Fighter",
                DescriptionLocaleId = "Super tough, hits things",
                Effects = effects
            };
            classTemplate.Variables.AssetCode("class-fighter");
            return classTemplate;
        }

        public ClassTemplate GenerateMageClass()
        {
            var effects = new[]
            {
                new StaticEffect {Potency = 4, EffectType = FantasyEffectTypes.IntelligenceBonusAmount},
                new StaticEffect {Potency = 10, EffectType = FantasyEffectTypes.DarkDamageAmount},
                new StaticEffect {Potency = 10, EffectType = FantasyEffectTypes.DarkDefenseAmount},
                new StaticEffect {Potency = 30, EffectType = FantasyEffectTypes.MagicBonusAmount}
            };

            var classTemplate = new ClassTemplate
            {
                Id = ClassTypeLookups.Mage,
                NameLocaleId = "Mage",
                DescriptionLocaleId = "Powerful magic users",
                Effects = effects
            };
            classTemplate.Variables.AssetCode("class-mage");
            return classTemplate;
        }


    }
}