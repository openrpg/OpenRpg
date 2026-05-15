using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Editor.Infrastructure.Editors;

namespace OpenRpg.Editor.Infrastructure.Services;

public class EditorPropertyResolver : IEditorPropertyResolver
{
    public Dictionary<string, PropertyInfo> BuildPropertyMap(Type templateType)
    {
        var map = new Dictionary<string, PropertyInfo>();
        var properties = templateType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var prop in properties)
        {
            map[prop.Name] = prop;
        }
        return map;
    }

    public string GetEditorType(PropertyInfo prop)
    {
        var name = prop.Name;

        if (TemplateEditorConventions.PropertyToEditorType.TryGetValue(name, out var conventionType))
            return conventionType;

        if (name.EndsWith("Type")) return "enumDropdown";

        if (prop.PropertyType.IsGenericType &&
            typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType))
            return "collection";

        if (prop.PropertyType == typeof(bool)) return "bool";
        if (prop.PropertyType == typeof(int)) return "scalar";

        return "unknown";
    }

    public Type GetCollectionElementType(PropertyInfo property)
    {
        var genericArgs = property.PropertyType.GetGenericArguments();
        return genericArgs.Length > 0 ? genericArgs[0] : typeof(object);
    }

    public string GetCollectionParamName(string propertyName, Type componentType)
    {
        if (componentType == null) return propertyName;

        var prop = componentType.GetProperty(propertyName);
        if (prop != null) return propertyName;

        var singularName = propertyName.TrimEnd('s');
        prop = componentType.GetProperty(singularName);
        if (prop != null) return singularName;

        var withoutAllowance = propertyName.Replace("Allowances", "Allowance");
        prop = componentType.GetProperty(withoutAllowance);
        if (prop != null) return withoutAllowance;

        var giftsToRewards = propertyName.Replace("Gifts", "Rewards");
        prop = componentType.GetProperty(giftsToRewards);
        if (prop != null) return giftsToRewards;

        var inputToTradeSkillItemEntries = propertyName.Replace("InputItems", "TradeSkillItemEntries");
        prop = componentType.GetProperty(inputToTradeSkillItemEntries);
        if (prop != null) return inputToTradeSkillItemEntries;

        var outputToTradeSkillItemEntries = propertyName.Replace("OutputItems", "TradeSkillItemEntries");
        prop = componentType.GetProperty(outputToTradeSkillItemEntries);
        if (prop != null) return outputToTradeSkillItemEntries;

        return propertyName;
    }

    public object GetNestedVariableContainer(ITemplateVariables variables, string containerName)
    {
        if (variables == null) return null;

        var variablesType = variables.GetType();

        var prop = variablesType.GetProperty(containerName);
        if (prop != null)
        {
            return prop.GetValue(variables);
        }

        if (containerName == "EquipmentSlots")
        {
            return GetOrCreateEquipmentSlots(variables);
        }

        var internalVarsProp = variablesType.GetProperty("InternalVariables");
        if (internalVarsProp != null)
        {
            var internalVars = internalVarsProp.GetValue(variables) as System.Collections.IDictionary;
            if (internalVars != null)
            {
                foreach (var key in internalVars.Keys)
                {
                    var value = internalVars[key];
                    if (value != null)
                    {
                        var valueType = value.GetType();
                        if (valueType.GetProperty("WeaponSlots") != null && valueType.GetProperty("MiscSlots") != null)
                        {
                            return value;
                        }
                    }
                }
            }
        }

        return null;
    }

    public object GetOrCreateEquipmentSlots(ITemplateVariables variables)
    {
        try
        {
            var variablesType = variables.GetType();
            var containsKeyMethod = variablesType.GetMethod("ContainsKey");
            if (containsKeyMethod == null) return null;

            const int equipmentSlotsKey = 1;
            var containsKey = (bool)containsKeyMethod.Invoke(variables, new object[] { equipmentSlotsKey });
            if (containsKey)
            {
                var indexer = variablesType.GetProperty("Item");
                return indexer?.GetValue(variables, new object[] { equipmentSlotsKey });
            }

            var addVariableMethod = variablesType.GetMethod("AddVariable");
            if (addVariableMethod == null) return null;

            var slotType = Type.GetType("OpenRpg.Genres.Scifi.Ships.ShipEquipmentSlots, OpenRpg.Genres.Scifi");
            if (slotType == null) return null;

            var slots = Activator.CreateInstance(slotType);
            addVariableMethod.Invoke(variables, new object[] { equipmentSlotsKey, slots });
            return slots;
        }
        catch
        {
            return null;
        }
    }
}
