using System;
using System.Collections.Generic;
using OpenRpg.Demos.Infrastructure.Extensions;
using OpenRpg.Demos.Infrastructure.Lookups;
using OpenRpg.Entities.Effects;
using OpenRpg.Entities.Modifications;
using OpenRpg.Entities.Requirements;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Templates;

namespace OpenRpg.Demos.Infrastructure.Data
{
    public class ItemTemplateDataGenerator : IDataGenerator<ItemTemplate>
    {
        public IEnumerable<ItemTemplate> GenerateData()
        {
            return new []
            {
                MakeRubbishSword(),
                MakeSuperSword(),
                MakePotion(),
                MakeJunkPotion(),
                MakeCopperIngot(),
                MakeCopperOre(),
                MakeIronOre(),
                MakeOakLog(),
                MakeCopperSword()
            };
        }

        public ItemTemplate MakeRubbishSword()
        {
            var template = new ItemTemplate
            {
                Id = ItemTemplateLookups.Sword,
                NameLocaleId = "Sword",
                DescriptionLocaleId = "A really bad looking sword, can slay things though",
                ItemType = FantasyItemTypes.GenericWeapon,
                ModificationAllowances = Array.Empty<ModificationAllowance>(),
                Requirements = Array.Empty<Requirement>(),
                Effects = new[]
                {
                    new StaticEffect { EffectType = FantasyEffectTypes.DamageBonusAmount, Potency = 30.0f }
                }
            };
            template.Variables.QualityType(FantasyItemQualityTypes.JunkQuality);
            template.Variables.Value(10);
            template.Variables.AssetCode("sword");

            return template;
        }

        private ItemTemplate MakeSuperSword()
        {
            var swordEffects = new[]
            {
                new StaticEffect { EffectType = FantasyEffectTypes.DamageBonusAmount, Potency = 765.5f },
                new StaticEffect { EffectType = FantasyEffectTypes.StrengthBonusAmount, Potency = 20.0f },
                new StaticEffect { EffectType = FantasyEffectTypes.DamageBonusPercentage, Potency = 10.0f },
                new StaticEffect { EffectType = FantasyEffectTypes.ConstitutionBonusAmount, Potency = 15.0f },
                new StaticEffect { EffectType = FantasyEffectTypes.DarkDefenseAmount, Potency = 15.0f }
            };

            var template = new ItemTemplate
            {
                Id = ItemTemplateLookups.SuperSword,
                NameLocaleId = "Super Sword",
                DescriptionLocaleId = "So fancy it could slice through stone",
                ItemType = FantasyItemTypes.GenericWeapon,
                ModificationAllowances = Array.Empty<ModificationAllowance>(),
                Requirements = Array.Empty<Requirement>(),
                Effects = swordEffects
            };
            template.Variables.QualityType(FantasyItemQualityTypes.EpicQuality);
            template.Variables.Value(10000);
            template.Variables.AssetCode("sword");
            
            return template;
        }

        public ItemTemplate MakePotion()
        {
            var template = new ItemTemplate
            {
                Id = ItemTemplateLookups.HealingPotion,
                NameLocaleId = "Healing Potion",
                DescriptionLocaleId = "A sketchy looking potion, lets hope it heals you",
                ItemType = FantasyItemTypes.Potions,
                ModificationAllowances = Array.Empty<ModificationAllowance>(),
                Requirements = Array.Empty<Requirement>(),
                Effects = new[]
                {
                new StaticEffect { EffectType = FantasyEffectTypes.HealthRestoreAmount, Potency = 30.0f }
            }
            };
            template.Variables.QualityType(FantasyItemQualityTypes.UncommonQuality);
            template.Variables.Value(20);
            template.Variables.MaxStacks(5);
            template.Variables.AssetCode("potion");

            return template;
        }

        public ItemTemplate MakeJunkPotion()
        {
            var template = new ItemTemplate
            {
                Id = ItemTemplateLookups.JunkPotion,
                NameLocaleId = "Junk Potion Combi-Deal",
                DescriptionLocaleId = "Who knows whats in this...",
                ItemType = FantasyItemTypes.Potions,
                ModificationAllowances = Array.Empty<ModificationAllowance>(),
                Requirements = Array.Empty<Requirement>(),
                Effects = new[]
                {
                new StaticEffect { EffectType = FantasyEffectTypes.HealthRestoreAmount, Potency = -5.0f }
            }
            };
            template.Variables.QualityType(FantasyItemQualityTypes.JunkQuality);
            template.Variables.Value(0);
            template.Variables.MaxStacks(5);
            template.Variables.AssetCode("potion-2");
            
            return template;
        }

        public ItemTemplate MakeCopperIngot()
        {
            var template = new ItemTemplate
            {
                Id = ItemTemplateLookups.CopperIngot,
                NameLocaleId = "Copper Ingot",
                DescriptionLocaleId = "A block of refined copper",
                ItemType = FantasyItemTypes.GenericItem,
                ModificationAllowances = Array.Empty<ModificationAllowance>(),
                Requirements = Array.Empty<Requirement>(),
                Effects = Array.Empty<StaticEffect>(),
            };
            template.Variables.QualityType(FantasyItemQualityTypes.CommonQuality);
            template.Variables.Value(5);
            template.Variables.MaxStacks(20);
            template.Variables.AssetCode("copper-ingot");
            
            return template;
        }

        public ItemTemplate MakeCopperOre()
        {
            var template = new ItemTemplate
            {
                Id = ItemTemplateLookups.CopperOre,
                NameLocaleId = "Copper Ore",
                DescriptionLocaleId = "A chunk of raw copper ore",
                ItemType = FantasyItemTypes.GenericItem,
                ModificationAllowances = Array.Empty<ModificationAllowance>(),
                Requirements = Array.Empty<Requirement>(),
                Effects = Array.Empty<StaticEffect>(),
            };
            template.Variables.QualityType(FantasyItemQualityTypes.CommonQuality);
            template.Variables.Value(1);
            template.Variables.MaxStacks(20);
            template.Variables.AssetCode("copper-ore");
            
            return template;
        }
        
        public ItemTemplate MakeIronOre()
        {
            var template = new ItemTemplate
            {
                Id = ItemTemplateLookups.IronOre,
                NameLocaleId = "Iron Ore",
                DescriptionLocaleId = "A chunk of raw iron ore",
                ItemType = FantasyItemTypes.GenericItem,
                ModificationAllowances = Array.Empty<ModificationAllowance>(),
                Requirements = Array.Empty<Requirement>(),
                Effects = Array.Empty<StaticEffect>(),
            };
            template.Variables.QualityType(FantasyItemQualityTypes.CommonQuality);
            template.Variables.Value(1);
            template.Variables.MaxStacks(20);
            template.Variables.AssetCode("iron-ore");
            
            return template;
        }

        public ItemTemplate MakeOakLog()
        {
            var template = new ItemTemplate
            {
                Id = ItemTemplateLookups.OakLog,
                NameLocaleId = "Oak Log",
                DescriptionLocaleId = "A log cut from an Oak tree",
                ItemType = FantasyItemTypes.GenericItem,
                ModificationAllowances = Array.Empty<ModificationAllowance>(),
                Requirements = Array.Empty<Requirement>(),
                Effects = Array.Empty<StaticEffect>(),
            };
            template.Variables.QualityType(FantasyItemQualityTypes.CommonQuality);
            template.Variables.Value(1);
            template.Variables.MaxStacks(20);
            template.Variables.AssetCode("oak-log");
            
            return template;
        }

        public ItemTemplate MakeCopperSword()
        {
            var template = new ItemTemplate
            {
                Id = ItemTemplateLookups.CopperSword,
                NameLocaleId = "A crafted copper sword",
                DescriptionLocaleId = "An oak hilt with a copper blade",
                ItemType = FantasyItemTypes.GenericWeapon,
                ModificationAllowances = Array.Empty<ModificationAllowance>(),
                Requirements = Array.Empty<Requirement>(),
                Effects = new[]
                {
                    new StaticEffect { EffectType = FantasyEffectTypes.DamageBonusAmount, Potency = 50 }
                }
            };
            template.Variables.QualityType(FantasyItemQualityTypes.CommonQuality);
            template.Variables.Value(50);
            template.Variables.MaxStacks(1);
            template.Variables.AssetCode("copper-sword");
            
            return template;
        }

    }
}