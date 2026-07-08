using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenRpg.Editor.Core.Plugins;

namespace OpenRpg.Editor.Infrastructure.Plugins;

public class GenreTypesProvider : IGenreTypesProvider
{
    public string PluginId { get; }
    
    public OptionData[] ItemTypes { get; }
    public OptionData[] ItemQualityTypes { get; }
    public OptionData[] RequirementTypes { get; }
    public OptionData[] EffectTypes { get; }
    public OptionData[] GenderTypes { get; }
    public OptionData[] RewardTypes { get; }
    public OptionData[] ModificationTypes { get; }
    public OptionData[] ObjectiveTypes { get; }
    public OptionData[] EffectScalingType { get; }
    public OptionData[] StatTypes { get; }
    public OptionData[] StateTypes { get; }
    public OptionData[] CraftingSkillTypes { get; }
    public OptionData[] GatheringSkillTypes { get; }
    public OptionData[] TargetTypes { get; }
    public OptionData[] DamageTypes { get; }
    public OptionData[] ItemSlotTypes { get; }

    public GenreTypesProvider(Assembly assembly, PluginManifest manifest)
    {
        PluginId = manifest.PluginId;
        
        ItemTypes = GetTypesFromSource(assembly, manifest.TypeSources, "itemTypes");
        ItemQualityTypes = GetTypesFromSource(assembly, manifest.TypeSources, "itemQualityTypes");
        RequirementTypes = GetTypesFromSource(assembly, manifest.TypeSources, "requirementTypes");
        EffectTypes = GetTypesFromSource(assembly, manifest.TypeSources, "effectTypes");
        GenderTypes = GetTypesFromSource(assembly, manifest.TypeSources, "genderTypes");
        RewardTypes = GetTypesFromSource(assembly, manifest.TypeSources, "rewardTypes");
        ModificationTypes = GetTypesFromSource(assembly, manifest.TypeSources, "modificationTypes");
        ObjectiveTypes = GetTypesFromSource(assembly, manifest.TypeSources, "objectiveTypes");
        EffectScalingType = GetTypesFromSource(assembly, manifest.TypeSources, "effectScalingType");
        StatTypes = GetTypesFromSource(assembly, manifest.TypeSources, "statTypes");
        StateTypes = GetTypesFromSource(assembly, manifest.TypeSources, "stateTypes");
        CraftingSkillTypes = GetTypesFromSource(assembly, manifest.TypeSources, "craftingSkillTypes");
        GatheringSkillTypes = GetTypesFromSource(assembly, manifest.TypeSources, "gatheringSkillTypes");
        TargetTypes = GetTypesFromSource(assembly, manifest.TypeSources, "targetTypes");
        DamageTypes = GetTypesFromSource(assembly, manifest.TypeSources, "damageTypes");
        ItemSlotTypes = GetTypesFromSource(assembly, manifest.TypeSources, "itemSlotTypes");
    }

    private static OptionData[] GetTypesFromSource(Assembly assembly, Dictionary<string, string> typeSources, string key)
    {
        if (!typeSources.TryGetValue(key, out var typeSource) || string.IsNullOrEmpty(typeSource))
        {
            return Array.Empty<OptionData>();
        }

        return TypeReflectionHelper.GetTypesFromAssembly(assembly, typeSource);
    }
}

public static class TypeReflectionHelper
{
    public static OptionData[] GetTypesFromAssembly(Assembly assembly, string typeSource)
    {
        var parts = typeSource.Split(',');
        if (parts.Length != 2)
        {
            return Array.Empty<OptionData>();
        }

        var typeName = parts[0].Trim();
        var assemblyName = parts[1].Trim();

        var targetAssembly = TryLoadAssembly(assemblyName) ?? assembly;
        var type = targetAssembly.GetType(typeName);
        if (type == null)
        {
            return Array.Empty<OptionData>();
        }

        return GetTypesFromInterface(type);
    }

    private static Assembly TryLoadAssembly(string assemblyName)
    {
        try
        {
            return Assembly.Load(assemblyName);
        }
        catch
        {
            return null;
        }
    }

    public static OptionData[] GetTypesFromInterface(Type interfaceType)
    {
        var fields = interfaceType.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)
            .Where(f => f.FieldType == typeof(int));

        return fields
            .Select(f => new OptionData(
                (int)f.GetValue(null)!,
                MakeReadable(f.Name)))
            .OrderBy(x => x.Id)
            .ToArray();
    }

    public static string MakeReadable(string name)
    {
        var result = new List<char>();
        for (int i = 0; i < name.Length; i++)
        {
            var c = name[i];
            if (i > 0 && char.IsUpper(c) && !char.IsUpper(name[i - 1]))
            {
                result.Add(' ');
            }
            result.Add(char.ToLower(c));
        }
        
        var resultStr = new string(result.ToArray());
        if (resultStr.Length > 0)
        {
            resultStr = char.ToUpper(resultStr[0]) + resultStr[1..];
        }
        return resultStr;
    }
}