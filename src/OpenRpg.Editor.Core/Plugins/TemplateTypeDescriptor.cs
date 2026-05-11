using System.Collections.Generic;
using System.Linq;

namespace OpenRpg.Editor.Core.Plugins;

public class TemplateTypeEntry
{
    public string Key { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string TemplateType { get; set; } = string.Empty;
    public string AssetCodePrefix { get; set; } = string.Empty;
    public List<SectionDefinition> Sections { get; set; } = new();
}

public class SectionDefinition
{
    public string Title { get; set; } = string.Empty;
    public string Property { get; set; } = string.Empty;
    public string EditorType { get; set; } = string.Empty;
    public string EditorComponent { get; set; } = string.Empty;
    public Dictionary<string, string> Options { get; set; } = new();
}

public interface ITemplateTypeDescriptor
{
    string Key { get; }
    string Name { get; }
    string TemplateTypeName { get; }
    string AssetCodePrefix { get; }
    IReadOnlyList<SectionDefinition> Sections { get; }
}

public class TemplateTypeDescriptor : ITemplateTypeDescriptor
{
    public string Key { get; }
    public string Name { get; }
    public string TemplateTypeName { get; }
    public string AssetCodePrefix { get; }
    public IReadOnlyList<SectionDefinition> Sections { get; }

    public TemplateTypeDescriptor(TemplateTypeEntry entry)
    {
        Key = entry.Key;
        Name = entry.Name;
        TemplateTypeName = entry.TemplateType;
        AssetCodePrefix = entry.AssetCodePrefix;
        Sections = entry.Sections.AsReadOnly();
    }

    public TemplateTypeDescriptor(string key, string name, string templateTypeName, string assetCodePrefix, IEnumerable<SectionDefinition> sections = null)
    {
        Key = key;
        Name = name;
        TemplateTypeName = templateTypeName;
        AssetCodePrefix = assetCodePrefix;
        Sections = (sections ?? Enumerable.Empty<SectionDefinition>()).ToList().AsReadOnly();
    }
}