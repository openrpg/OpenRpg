namespace OpenRpg.Editor.Core.Plugins;

public class TemplateTypeEntry
{
    public string Key { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string TemplateType { get; set; } = string.Empty;
    public string AssetCodePrefix { get; set; } = string.Empty;
}

public interface ITemplateTypeDescriptor
{
    string Key { get; }
    string Name { get; }
    string TemplateTypeName { get; }
    string AssetCodePrefix { get; }
}

public class TemplateTypeDescriptor : ITemplateTypeDescriptor
{
    public string Key { get; }
    public string Name { get; }
    public string TemplateTypeName { get; }
    public string AssetCodePrefix { get; }

    public TemplateTypeDescriptor(TemplateTypeEntry entry)
    {
        Key = entry.Key;
        Name = entry.Name;
        TemplateTypeName = entry.TemplateType;
        AssetCodePrefix = entry.AssetCodePrefix;
    }

    public TemplateTypeDescriptor(string key, string name, string templateTypeName, string assetCodePrefix)
    {
        Key = key;
        Name = name;
        TemplateTypeName = templateTypeName;
        AssetCodePrefix = assetCodePrefix;
    }
}