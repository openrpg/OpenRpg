using System;
using System.Collections.Generic;
using System.Reflection;
using OpenRpg.Editor.Core.Plugins;

namespace OpenRpg.Editor.Infrastructure.Editors;

public static class TemplateEditorConventions
{
    public static readonly Dictionary<string, string> PropertyToEditorType = new()
    {
        ["Objectives"] = "collection",
        ["Rewards"] = "collection",
        ["Gifts"] = "collection",
        ["InputItems"] = "collection",
        ["OutputItems"] = "collection",
        ["ModificationAllowances"] = "collection",
    };

    public static readonly Dictionary<string, string> CollectionTypeToComponent = new()
    {
        ["Objective"] = "ObjectivesEditor",
        ["Reward"] = "RewardsEditor",
        ["TradeSkillItemEntry"] = "TradeSkillItemEntriesEditor",
        ["ModificationAllowance"] = "ModificationAllowancesEditor",
        ["IEffect"] = "EffectsEditor",
        ["Requirement"] = "RequirementsEditor",
    };

    public static readonly Dictionary<string, string> PropertyToTypeSource = new()
    {
        ["ItemType"] = "itemTypes",
        ["QualityType"] = "itemQualityTypes",
        ["ModificationType"] = "modificationTypes",
        ["EffectType"] = "effectTypes",
        ["RequirementType"] = "requirementTypes",
        ["ObjectiveType"] = "objectiveTypes",
        ["RewardType"] = "rewardTypes",
        ["SkillType"] = "gatheringSkillTypes",
        ["ScalingType"] = "effectScalingType",
    };

    public static readonly Dictionary<string, string> VariableFieldToLabel = new()
    {
        ["MaxStacks"] = "Stack Amount",
        ["Value"] = "Value",
        ["QualityType"] = "Item Quality",
        ["Weight"] = "Weight",
    };

    public static readonly Dictionary<string, string> VariableFieldToTypeSource = new()
    {
        ["QualityType"] = "itemQualityTypes",
    };

    public static string GetEditorType(string propertyName)
    {
        if (PropertyToEditorType.TryGetValue(propertyName, out var editorType))
            return editorType;
        
        if (propertyName.EndsWith("Type"))
            return "enumDropdown";
        
        if (propertyName.StartsWith("Variables."))
            return "variableField";

        return "scalar";
    }

    public static string GetTypeSource(string propertyName)
    {
        if (PropertyToTypeSource.TryGetValue(propertyName, out var typeSource))
            return typeSource;

        if (propertyName.StartsWith("Variables.") && propertyName.Contains("Type"))
        {
            var fieldName = propertyName.Replace("Variables.", "");
            if (VariableFieldToTypeSource.TryGetValue(fieldName, out var fieldTypeSource))
                return fieldTypeSource;
        }

        return string.Empty;
    }

    public static string GetCollectionComponent(Type elementType)
    {
        var typeName = elementType.Name;
        
        if (CollectionTypeToComponent.TryGetValue(typeName, out var component))
            return component;

        if (typeName.Contains("Effect"))
            return "EffectsEditor";
        if (typeName.Contains("Requirement"))
            return "RequirementsEditor";

        return string.Empty;
    }

    public static string GetLabel(string propertyName)
    {
        if (propertyName.StartsWith("Variables."))
        {
            var fieldName = propertyName.Replace("Variables.", "");
            if (VariableFieldToLabel.TryGetValue(fieldName, out var label))
                return label;
            return fieldName;
        }

        return propertyName;
    }

    public static bool IsCollection(PropertyInfo property)
    {
        return property.PropertyType.IsGenericType && 
               typeof(System.Collections.IEnumerable).IsAssignableFrom(property.PropertyType);
    }

    public static bool IsEnumDropdown(PropertyInfo property)
    {
        return property.PropertyType == typeof(int);
    }

    public static Type GetCollectionElementType(PropertyInfo property)
    {
        var genericArgs = property.PropertyType.GetGenericArguments();
        return genericArgs.Length > 0 ? genericArgs[0] : typeof(object);
    }
}