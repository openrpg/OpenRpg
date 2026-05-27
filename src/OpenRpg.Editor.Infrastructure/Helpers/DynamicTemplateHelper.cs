using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Data;
using OpenRpg.Data.Conventions.Extensions;
using OpenRpg.Editor.Infrastructure.Extensions;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Types;
using OpenRpg.Items.Templates;
using OpenRpg.Items.TradeSkills.Templates;
using OpenRpg.Quests;

namespace OpenRpg.Editor.Infrastructure.Helpers;

public class DynamicTemplateHelper
{
    private readonly ILogger<DynamicTemplateHelper> _logger;

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

    public DynamicTemplateHelper(Type templateType, IRepository repository, ILogger<DynamicTemplateHelper> logger)
    {
        TemplateType = templateType;
        Repository = repository;
        _logger = logger;

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
    {
        var results = (IEnumerable<ITemplate>) _getAllMethod.Invoke(null, [Repository]);
        _logger.LogDebug("GetAll for {TemplateType} returned {Count} result(s)", TemplateType.Name, results.Count());
        return results;
    }

    public ITemplate Create(ITemplate template, object id)
    {
        _logger.LogDebug("Creating {TemplateType} with Id {Id}", TemplateType.Name, id);
        return (ITemplate) _createMethod.Invoke(null, [Repository, template, id]);
    }

    public void Delete(ITemplate template)
    {
        _logger.LogDebug("Deleting {TemplateType} Id {Id}", TemplateType.Name, template.Id);
        _deleteMethod.Invoke(null, [Repository, template.Id]);
    }

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
        if (template is IHasVariables<ITemplateVariables> hasVars)
        {
            var variables = hasVars.Variables;
            if (!variables.ContainsKey(CoreTemplateVariableTypes.Effects))
            { variables[CoreTemplateVariableTypes.Effects] = new List<IEffect>(); }
            if (!variables.ContainsKey(CoreTemplateVariableTypes.Requirements))
            { variables[CoreTemplateVariableTypes.Requirements] = new List<Requirement>(); }
        }

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

    public void GenerateLocaleCodes(ITemplate template)
    {
        if(template.GetType() != TemplateType)
        { throw new ArgumentException("Template type does not match helper internal type"); }

        var variables = GetVariables(template);
        var assetCode = variables.AssetCode;

        if (string.IsNullOrEmpty(assetCode))
        {
            _logger.LogWarning("Could not generate locale codes for {TemplateType} Id {Id} - no asset code set",
                TemplateType.Name, template.Id);
            return;
        }

        _templateNameLocaleIdPropertyAccessor.SetValue(template, $"{assetCode}-name");
        _templateDescriptionLocaleIdPropertyAccessor.SetValue(template, $"{assetCode}-description");

        _logger.LogDebug("Generated locale codes '{NameLocale}' and '{DescLocale}' for {TemplateType} Id {Id}",
            $"{assetCode}-name", $"{assetCode}-description", TemplateType.Name, template.Id);
    }
}