using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Abilities;
using OpenRpg.Combat.Extensions;
using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Templates.Variables;
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
            var type = localeEntity.GetType();
            var nameUpdaterProperty = type.GetProperty(nameof(IHasLocaleDescription.NameLocaleId));
            var descriptionUpdaterProperty = type.GetProperty(nameof(IHasLocaleDescription.DescriptionLocaleId));
            
            nameUpdaterProperty.SetValue(localeEntity, $"{newAssetCode}-name");
            descriptionUpdaterProperty.SetValue(localeEntity, $"{newAssetCode}-description");
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
            var dataObjectType = dataObject.GetType();
            var idProperty = dataObjectType.GetProperty("Id");
            idProperty.SetValue(dataObject, id);
        }
        
        public static void ListifyProperties(this ITemplate template)
        {
            var templateType = template.GetType();
            var variableProperty = templateType.GetProperty("Variables");
            var variables = variableProperty.GetValue(template) as ITemplateVariables;
                
            if(variables.HasEffects())
            { variables.Effects = variables.Effects.AsList(); }
            else
            { variables.Effects = new List<IEffect>(); }
            
            if(variables.HasRequirements())
            { variables.Requirements = variables.Requirements.ToList(); }
            else
            { variables.Requirements = new List<Requirement>(); }
            
            if(variables.HasAbilities())
            { variables.Abilities = variables.Abilities.ToList(); }
            else
            { variables.Abilities = new List<AbilityData>(); }
            
            if (template is ItemTemplate itemTemplate)
            { itemTemplate.ModificationAllowances = itemTemplate.ModificationAllowances.AsList(); }
            else if (template is Quest quest)
            {
                quest.Gifts = quest.Gifts.AsList();
                quest.Objectives = quest.Objectives.AsList();
                quest.Rewards = quest.Rewards.AsList();
            }
            else if (template is ItemCraftingTemplate craftingTemplate)
            {
                craftingTemplate.InputItems = craftingTemplate.InputItems.AsList();
                craftingTemplate.OutputItems = craftingTemplate.OutputItems.AsList();
            }
            else if (template is ItemGatheringTemplate gatheringTemplate)
            {
                gatheringTemplate.OutputItems = gatheringTemplate.OutputItems.AsList();
            }
        }
    }
}