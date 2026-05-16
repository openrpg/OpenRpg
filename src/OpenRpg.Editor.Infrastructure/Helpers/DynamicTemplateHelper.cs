using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenRpg.Combat.Abilities;
using OpenRpg.Combat.Extensions;
using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Data;
using OpenRpg.Data.Conventions.Extensions;
using OpenRpg.Editor.Infrastructure.Extensions;
using OpenRpg.Entities.Extensions;
using OpenRpg.Items.Templates;
using OpenRpg.Items.TradeSkills.Templates;
using OpenRpg.Quests;

namespace OpenRpg.Editor.Infrastructure.Helpers;

public class DynamicTemplateHelper
{
    public Type TemplateType { get; set; }
    public IRepository Repository { get; set; }

    private PropertyInfo _templateIdPropertyAccessor;
    private PropertyInfo _templateNameLocaleIdPropertyAccessor;
    private PropertyInfo _templateDescriptionLocaleIdPropertyAccessor;
    private PropertyInfo _variablesPropertyAccessor;
    
    private MethodInfo _getAllMethod;
    private MethodInfo _createMethod;
    private MethodInfo _deleteMethod;
    private MethodInfo _existsMethod;
    
    public DynamicTemplateHelper(Type templateType, IRepository repository)
    {
        TemplateType = templateType;
        Repository = repository;

        _templateIdPropertyAccessor = TemplateType.GetProperty(nameof(ITemplate.Id));
        if(_templateIdPropertyAccessor?.CanWrite == false)
        { throw new Exception($"Unable to write Id property for [{TemplateType.Name}]"); }
        
        _templateNameLocaleIdPropertyAccessor = TemplateType.GetProperty(nameof(ITemplate.NameLocaleId));
        if(_templateNameLocaleIdPropertyAccessor?.CanWrite == false)
        { throw new Exception($"Unable to write NameLocaleId property for [{TemplateType.Name}]"); }

        _templateDescriptionLocaleIdPropertyAccessor = TemplateType.GetProperty(nameof(ITemplate.DescriptionLocaleId));
        if(_templateDescriptionLocaleIdPropertyAccessor?.CanWrite == false)
        { throw new Exception($"Unable to write DescriptionLocaleId property for [{TemplateType.Name}]"); }


        var variablesPropertyName = nameof(ITemplate<>.Variables);
        _variablesPropertyAccessor = TemplateType.GetProperty(variablesPropertyName);
        if(_variablesPropertyAccessor == null) 
        { throw new Exception($"Unable to find [{variablesPropertyName}] property for [{TemplateType.Name}]"); }
        
        _getAllMethod = typeof(RepositoryExtensions).GetMethod(nameof(RepositoryExtensions.GetAll))?.MakeGenericMethod(TemplateType);
        _createMethod = typeof(RepositoryExtensions).GetMethod(nameof(RepositoryExtensions.Create))?.MakeGenericMethod(TemplateType);
        _deleteMethod = typeof(RepositoryExtensions).GetMethod(nameof(RepositoryExtensions.Delete))?.MakeGenericMethod(TemplateType);
        _existsMethod = typeof(RepositoryExtensions).GetMethod(nameof(RepositoryExtensions.Exists))?.MakeGenericMethod(TemplateType);
    }
    
    public IEnumerable<ITemplate> GetAll()
    { return (IEnumerable<ITemplate>) _getAllMethod.Invoke(null, [Repository]); }
    
    public ITemplate Create(ITemplate template, object id)
    { return (ITemplate) _createMethod.Invoke(null, [Repository, template, id]); }
    
    public void Delete(ITemplate template)
    { _deleteMethod.Invoke(null, [Repository, template.Id]); }
    
    public void Exists(object id)
    { _existsMethod.Invoke(null, [Repository, id]); }

    public void SetId(ITemplate template, int id)
    {
        if(template.GetType() != TemplateType) 
        { throw new ArgumentException("Template type does not match helper internal type"); }
        
        _templateIdPropertyAccessor.SetValue(template, id);
    }
    
    public int GetNewId()
    {
        var idsInUse = GetAll().Select(x => x.Id).ToHashSet();
        var newId = 1;
        while (idsInUse.Contains(newId)) { newId++; }
        return newId;
    }

    public ITemplateVariables GetVariables(ITemplate template)
    {
        if(template.GetType() != TemplateType) 
        { throw new ArgumentException("Template type does not match helper internal type"); }
        return (ITemplateVariables) _variablesPropertyAccessor.GetValue(template);
    }

    public void ListifyProperties(ITemplate template)
    {
        var variables = GetVariables(template);
        
        variables.Effects = variables.HasEffects() ? variables.Effects.AsList() : new List<IEffect>();
        variables.Requirements = variables.HasRequirements() ? variables.Requirements.AsList() : new List<Requirement>();
        variables.Abilities = variables.HasAbilities() ? variables.Abilities.AsList() : new List<AbilityData>();
        
        if (template is ItemTemplate itemTemplate)
        {
            itemTemplate.ModificationAllowances = itemTemplate.ModificationAllowances.AsList();
            return;
        }
        
        if (template is Quest quest)
        {
            quest.Gifts = quest.Gifts.AsList();
            quest.Objectives = quest.Objectives.AsList();
            quest.Rewards = quest.Rewards.AsList();
            quest.Variables.Requirements = quest.Variables.Requirements.AsList();
            return;
        }

        if (template is ItemCraftingTemplate craftingTemplate)
        {
            craftingTemplate.InputItems = craftingTemplate.InputItems.AsList();
            craftingTemplate.OutputItems = craftingTemplate.OutputItems.AsList();
            return;
        }

        if (template is ItemGatheringTemplate gatheringTemplate)
        {
            gatheringTemplate.OutputItems = gatheringTemplate.OutputItems.AsList();
            return;
        }
    }
    
    public void GenerateLocaleCodes(ITemplate template)
    {
        if(template.GetType() != TemplateType) 
        { throw new ArgumentException("Template type does not match helper internal type"); }
            
        var variables = GetVariables(template);
        var assetCode = variables.AssetCode;
        
        _templateNameLocaleIdPropertyAccessor.SetValue(template, $"{assetCode}-name");
        _templateDescriptionLocaleIdPropertyAccessor.SetValue(template, $"{assetCode}-description");
    }
}