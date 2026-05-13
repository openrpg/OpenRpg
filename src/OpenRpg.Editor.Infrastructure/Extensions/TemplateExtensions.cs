using OpenRpg.Core.Common;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Classes.Templates;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Races.Templates;
using OpenRpg.Items.Templates;
using OpenRpg.Items.TradeSkills.Templates;
using OpenRpg.Localization.Data.Extensions;
using OpenRpg.Localization.Data.Repositories;
using OpenRpg.Quests;

namespace OpenRpg.Editor.Infrastructure.Extensions
{
    public static class TemplateExtensions
    {
        public static void GenerateLocaleCodes<T>(this T localeEntity, string newAssetCode) where T : IHasLocaleDescription
        {
            if (localeEntity is ItemTemplate itemTemplate)
            {
                itemTemplate.NameLocaleId = $"{newAssetCode}-name";
                itemTemplate.DescriptionLocaleId = $"{newAssetCode}-description";
            }
            else if (localeEntity is ClassTemplate classTemplate)
            {
                classTemplate.NameLocaleId = $"{newAssetCode}-name";
                classTemplate.DescriptionLocaleId = $"{newAssetCode}-description";
            }
            else if (localeEntity is RaceTemplate raceTemplate)
            {
                raceTemplate.NameLocaleId = $"{newAssetCode}-name";
                raceTemplate.DescriptionLocaleId = $"{newAssetCode}-description";
            }
            else if (localeEntity is Quest quest)
            {
                quest.NameLocaleId = $"{newAssetCode}-name";
                quest.DescriptionLocaleId = $"{newAssetCode}-description";
            }
            else if (localeEntity is ItemCraftingTemplate craftingTemplate)
            {
                craftingTemplate.NameLocaleId = $"{newAssetCode}-name";
                craftingTemplate.DescriptionLocaleId = $"{newAssetCode}-description";
            }
            else if (localeEntity is ItemGatheringTemplate gatheringTemplate)
            {
                gatheringTemplate.NameLocaleId = $"{newAssetCode}-name";
                gatheringTemplate.DescriptionLocaleId = $"{newAssetCode}-description";
            }
        }

        public static void UpdateLocale<T>(this T localeEntity, string newAssetCode, ILocaleRepository repository)
            where T : IHasLocaleDescription
        {
            var oldNameLocaleId = localeEntity.NameLocaleId;
            var oldDescriptionLocaleId = localeEntity.DescriptionLocaleId;

            localeEntity.GenerateLocaleCodes(newAssetCode);
            
            MoveLocaleInRepository(repository, oldNameLocaleId, localeEntity.NameLocaleId);
            MoveLocaleInRepository(repository, oldDescriptionLocaleId, localeEntity.DescriptionLocaleId);
        }

        public static void MoveLocaleInRepository(this ILocaleRepository repository, string oldLocaleId, string newLocaleId)
        {
            if (!repository.Exists(oldLocaleId)) { return; }
            var localeData = repository.Get(oldLocaleId);
            repository.Delete(oldLocaleId);
            repository.Create(newLocaleId, localeData);
        }
        
        public static void SetId(this IHasDataId dataObject, int id)
        {
            if (dataObject is ItemTemplate itemTemplate)
            { itemTemplate.Id = id; }
            else if (dataObject is ClassTemplate classTemplate)
            { classTemplate.Id = id; }
            else if (dataObject is RaceTemplate raceTemplate)
            { raceTemplate.Id = id; }
            else if (dataObject is Quest quest)
            { quest.Id = id; }
            else if (dataObject is ItemCraftingTemplate craftingTemplate)
            { craftingTemplate.Id = id; }
            else if (dataObject is ItemGatheringTemplate gatheringTemplate)
            { gatheringTemplate.Id = id; }
        }
        
        public static void ListifyProperties(this ITemplate template)
        {
            if (template is ItemTemplate itemTemplate)
            {
                itemTemplate.Variables.Effects = itemTemplate.Variables.Effects.AsList();
                itemTemplate.Variables.Requirements = itemTemplate.Variables.Requirements.AsList();
                itemTemplate.ModificationAllowances = itemTemplate.ModificationAllowances.AsList();
            }
            else if (template is ClassTemplate classTemplate)
            {
                classTemplate.Variables.Effects = classTemplate.Variables.Effects.AsList();
                classTemplate.Variables.Requirements = classTemplate.Variables.Requirements.AsList();
            }
            else if (template is RaceTemplate raceTemplate)
            {
                raceTemplate.Variables.Effects = raceTemplate.Variables.Effects.AsList();
                raceTemplate.Variables.Requirements = raceTemplate.Variables.Requirements.AsList();
            }
            else if (template is Quest quest)
            {
                quest.Gifts = quest.Gifts.AsList();
                quest.Objectives = quest.Objectives.AsList();
                quest.Rewards = quest.Rewards.AsList();
                quest.Variables.Requirements = quest.Variables.Requirements.AsList();
            }
            else if (template is ItemCraftingTemplate craftingTemplate)
            {
                craftingTemplate.Variables.Requirements = craftingTemplate.Variables.Requirements.AsList();
            }
            else if (template is ItemGatheringTemplate gatheringTemplate)
            {
                gatheringTemplate.Variables.Requirements = gatheringTemplate.Variables.Requirements.AsList();
            }
            else if (template is OpenRpg.Genres.Scifi.Ships.ShipTemplate shipTemplate)
            {
                shipTemplate.Variables.Effects = shipTemplate.Variables.Effects.AsList();
                shipTemplate.Variables.Requirements = shipTemplate.Variables.Requirements.AsList();
            }
        }
    }
}