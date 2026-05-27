using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using OpenRpg.Core.Templates;
using OpenRpg.Editor.Core.Models;
using OpenRpg.Editor.Infrastructure.Data;
using OpenRpg.Entities.Types;
using OpenRpg.Projects.Models;

namespace OpenRpg.Editor.Infrastructure.Services.Generators;

public class ProjectFileGenerator : IProjectFileGenerator
{
    private const string DefaultNamespace = "OpenRpg.Project.Lookups";

    public IReadOnlyCollection<GeneratedFile> GenerateFiles(ProjectContext context, EditorDatasource dataSource)
    {
        var results = new List<GeneratedFile>();
        var templateInterface = typeof(ITemplate);

        foreach (var (type, instances) in dataSource.Database)
        {
            if (!templateInterface.IsAssignableFrom(type)) continue;
            if (instances.Count == 0) continue;

            var entries = BuildEntries(type, instances);
            if (entries.Count == 0) continue;

            results.Add(BuildGeneratedFile(type, entries));
        }

        return results;
    }

    private static List<(string Name, int Id)> BuildEntries(Type type, Dictionary<object, object> instances)
    {
        var idProperty = type.GetProperty("Id");
        var variablesProperty = type.GetProperty("Variables");

        if (idProperty == null || variablesProperty == null) return new List<(string, int)>();

        var entries = new List<(string Name, int Id)>();

        foreach (var instance in instances.Values)
        {
            var id = (int)idProperty.GetValue(instance)!;
            var pascalName = ResolvePascalName(variablesProperty, instance);
            if (string.IsNullOrEmpty(pascalName)) continue;
            entries.Add((pascalName, id));
        }

        // Sort alphabetically
        entries = entries
            .OrderBy(e => e.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();

        // Resolve duplicate names by appending _{id}
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (var i = 0; i < entries.Count; i++)
        {
            var (name, id) = entries[i];
            if (seen.Add(name)) { continue; }
            entries[i] = ($"{name}_{id}", id);
            seen.Add(entries[i].Name);
        }

        return entries;
    }

    private static string ResolvePascalName(PropertyInfo variablesProperty, object instance)
    {
        var variables = variablesProperty.GetValue(instance);
        if (variables == null) return string.Empty;

        // Variables implements IReadOnlyDictionary<int, object> via IVariables<object>
        var dictionary = variables as IReadOnlyDictionary<int, object>;
        if (dictionary == null || !dictionary.TryGetValue(CoreAnyVariableTypes.AssetCode, out var assetCodeObj))
        { return string.Empty; }

        return AssetCodeToPascalCase(assetCodeObj?.ToString());
    }

    private static string AssetCodeToPascalCase(string assetCode)
    {
        if (string.IsNullOrWhiteSpace(assetCode)) return string.Empty;

        var parts = assetCode.Split(['_', '-', ' '], StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0) return string.Empty;

        return string.Concat(parts.Select(p =>
            char.ToUpperInvariant(p[0]) + p[1..].ToLowerInvariant()));
    }

    private static GeneratedFile BuildGeneratedFile(Type type, List<(string Name, int Id)> entries)
    {
        var className = type.Name + "Lookups";
        var sb = new StringBuilder();

        sb.AppendLine($"namespace {DefaultNamespace};");
        sb.AppendLine();
        sb.AppendLine($"public static class {className}");
        sb.AppendLine("{");

        // Unknown sentinel
        sb.AppendLine("    public const int Unknown = 0;");
        sb.AppendLine();

        // Const entries (skip id=0 entries already covered by Unknown)
        foreach (var (name, id) in entries)
        {
            if (id == 0 && name == "Unknown") continue;
            sb.AppendLine($"    public const int {name} = {id};");
        }

        sb.AppendLine();
        sb.Append("    public static readonly int[] All");
        sb.Append(type.Name);
        sb.Append("Ids = [");
        sb.Append(string.Join(", ", entries.Where(e => !(e.Id == 0 && e.Name == "Unknown")).Select(e => e.Name)));
        sb.AppendLine("];");

        sb.AppendLine("}");

        return new GeneratedFile
        {
            Filename = $"{className}.cs",
            Path = "",
            Content = sb.ToString()
        };
    }
}
