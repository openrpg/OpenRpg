using System.Collections.Generic;
using OpenRpg.Demos.Infrastructure.Extensions;
using OpenRpg.Demos.Infrastructure.Lookups;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Races.Templates;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Demos.Infrastructure.Data
{
    public class RaceTemplateDataGenerator : IDataGenerator<RaceTemplate>
    {
        public IEnumerable<RaceTemplate> GenerateData()
        {
            return new [] 
            {
                GenerateHumanTemplate(),
                GenerateElfTemplate(),
                GenerateDwarfTemplate(),
            };
        }

        public RaceTemplate GenerateHumanTemplate()
        {
            var effects = new[]
            {
                new StaticEffect {Potency = 10, EffectType = FantasyEffectTypes.AllAttributeBonusAmount},
                new StaticEffect {Potency = 8, EffectType = FantasyEffectTypes.DamageBonusAmount},
                new StaticEffect {Potency = 8, EffectType = FantasyEffectTypes.DefenseBonusAmount},
                new StaticEffect {Potency = 1, EffectType = FantasyEffectTypes.DarkDamageAmount},
                new StaticEffect {Potency = 5, EffectType = FantasyEffectTypes.DarkDefenseAmount},
                new StaticEffect {Potency = 80, EffectType = FantasyEffectTypes.HealthBonusAmount},
                new StaticEffect {Potency = 10, EffectType = FantasyEffectTypes.MagicBonusAmount}
            };

            var raceTemplate = new RaceTemplate
            {
                Id = RaceTypeLookups.Human,
                NameLocaleId = "Human",
                DescriptionLocaleId = "Humans are the most common of all races",
                Effects = effects
            };
            raceTemplate.Variables.AssetCode("race-human");
            return raceTemplate;
        }

        public RaceTemplate GenerateElfTemplate()
        {
            var effects = new[]
            {
                new StaticEffect {Potency = 8, EffectType = FantasyEffectTypes.StrengthBonusAmount},
                new StaticEffect {Potency = 12, EffectType = FantasyEffectTypes.DexterityBonusAmount},
                new StaticEffect {Potency = 8, EffectType = FantasyEffectTypes.ConstitutionBonusAmount},
                new StaticEffect {Potency = 12, EffectType = FantasyEffectTypes.IntelligenceBonusAmount},
                new StaticEffect {Potency = 10, EffectType = FantasyEffectTypes.WisdomBonusAmount},
                new StaticEffect {Potency = 10, EffectType = FantasyEffectTypes.CharismaBonusAmount},
                new StaticEffect {Potency = 8, EffectType = FantasyEffectTypes.DamageBonusAmount},
                new StaticEffect {Potency = 7, EffectType = FantasyEffectTypes.DefenseBonusAmount},
                new StaticEffect {Potency = 5, EffectType = FantasyEffectTypes.DarkDamageAmount},
                new StaticEffect {Potency = 10, EffectType = FantasyEffectTypes.DarkDefenseAmount},
                new StaticEffect {Potency = 70, EffectType = FantasyEffectTypes.HealthBonusAmount},
                new StaticEffect {Potency = 30, EffectType = FantasyEffectTypes.MagicBonusAmount}
            };

            var raceTemplate = new RaceTemplate
            {
                Id = RaceTypeLookups.Elf,
                NameLocaleId = "Elf",
                DescriptionLocaleId = "Elves are pretty common, have pointy ears too",
                Effects = effects
            };
            raceTemplate.Variables.AssetCode("race-elf");
            return raceTemplate;
        }

        public RaceTemplate GenerateDwarfTemplate()
        {
            var effects = new[]
            {
                new StaticEffect {Potency = 12, EffectType = FantasyEffectTypes.StrengthBonusAmount},
                new StaticEffect {Potency = 8, EffectType = FantasyEffectTypes.DexterityBonusAmount},
                new StaticEffect {Potency = 12, EffectType = FantasyEffectTypes.ConstitutionBonusAmount},
                new StaticEffect {Potency = 10, EffectType = FantasyEffectTypes.IntelligenceBonusAmount},
                new StaticEffect {Potency = 10, EffectType = FantasyEffectTypes.WisdomBonusAmount},
                new StaticEffect {Potency = 8, EffectType = FantasyEffectTypes.CharismaBonusAmount},
                new StaticEffect {Potency = 8, EffectType = FantasyEffectTypes.DamageBonusAmount},
                new StaticEffect {Potency = 10, EffectType = FantasyEffectTypes.DefenseBonusAmount},
                new StaticEffect {Potency = 100, EffectType = FantasyEffectTypes.HealthBonusAmount},
                new StaticEffect {Potency = -1, EffectType = FantasyEffectTypes.DarkDefenseAmount},

            };

            var raceTemplate = new RaceTemplate
            {
                Id = RaceTypeLookups.Dwarf,
                NameLocaleId = "Dwarf",
                DescriptionLocaleId = "Dwarves are strong and hardy",
                Effects = effects
            };
            raceTemplate.Variables.AssetCode("race-dwarf");
            return raceTemplate;
        }
    }
}