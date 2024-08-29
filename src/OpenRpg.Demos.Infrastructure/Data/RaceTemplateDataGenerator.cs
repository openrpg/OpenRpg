using System.Collections.Generic;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Races.Templates;
using OpenRpg.Demos.Infrastructure.Extensions;
using OpenRpg.Demos.Infrastructure.Lookups;
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
                new Effect {Potency = 10, EffectType = FantasyEffectTypes.AllAttributeBonusAmount},
                new Effect {Potency = 8, EffectType = FantasyEffectTypes.DamageBonusAmount},
                new Effect {Potency = 8, EffectType = FantasyEffectTypes.DefenseBonusAmount},
                new Effect {Potency = 1, EffectType = FantasyEffectTypes.DarkDamageAmount},
                new Effect {Potency = 5, EffectType = FantasyEffectTypes.DarkDefenseAmount},
                new Effect {Potency = 80, EffectType = FantasyEffectTypes.HealthBonusAmount},
                new Effect {Potency = 10, EffectType = FantasyEffectTypes.MagicBonusAmount}
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
                new Effect {Potency = 8, EffectType = FantasyEffectTypes.StrengthBonusAmount},
                new Effect {Potency = 12, EffectType = FantasyEffectTypes.DexterityBonusAmount},
                new Effect {Potency = 8, EffectType = FantasyEffectTypes.ConstitutionBonusAmount},
                new Effect {Potency = 12, EffectType = FantasyEffectTypes.IntelligenceBonusAmount},
                new Effect {Potency = 10, EffectType = FantasyEffectTypes.WisdomBonusAmount},
                new Effect {Potency = 10, EffectType = FantasyEffectTypes.CharismaBonusAmount},
                new Effect {Potency = 8, EffectType = FantasyEffectTypes.DamageBonusAmount},
                new Effect {Potency = 7, EffectType = FantasyEffectTypes.DefenseBonusAmount},
                new Effect {Potency = 5, EffectType = FantasyEffectTypes.DarkDamageAmount},
                new Effect {Potency = 10, EffectType = FantasyEffectTypes.DarkDefenseAmount},
                new Effect {Potency = 70, EffectType = FantasyEffectTypes.HealthBonusAmount},
                new Effect {Potency = 30, EffectType = FantasyEffectTypes.MagicBonusAmount}
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
                new Effect {Potency = 12, EffectType = FantasyEffectTypes.StrengthBonusAmount},
                new Effect {Potency = 8, EffectType = FantasyEffectTypes.DexterityBonusAmount},
                new Effect {Potency = 12, EffectType = FantasyEffectTypes.ConstitutionBonusAmount},
                new Effect {Potency = 10, EffectType = FantasyEffectTypes.IntelligenceBonusAmount},
                new Effect {Potency = 10, EffectType = FantasyEffectTypes.WisdomBonusAmount},
                new Effect {Potency = 8, EffectType = FantasyEffectTypes.CharismaBonusAmount},
                new Effect {Potency = 8, EffectType = FantasyEffectTypes.DamageBonusAmount},
                new Effect {Potency = 10, EffectType = FantasyEffectTypes.DefenseBonusAmount},
                new Effect {Potency = 100, EffectType = FantasyEffectTypes.HealthBonusAmount},
                new Effect {Potency = -1, EffectType = FantasyEffectTypes.DarkDefenseAmount},

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