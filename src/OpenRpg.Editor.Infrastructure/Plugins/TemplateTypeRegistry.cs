using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenRpg.Editor.Core.Plugins;

namespace OpenRpg.Editor.Infrastructure.Plugins;

public interface ITemplateTypeRegistry
{
    IReadOnlyList<ITemplateTypeDescriptor> GetTemplateTypes();
    ITemplateTypeDescriptor GetTemplateType(string key);
    Type GetTemplateClassType(ITemplateTypeDescriptor descriptor);
}

public class TemplateTypeRegistry : ITemplateTypeRegistry
{
    private readonly List<ITemplateTypeDescriptor> _templateTypes = new();

    public TemplateTypeRegistry(IEnumerable<EditorPluginInfo> plugins)
    {
        foreach (var plugin in plugins)
        {
            _templateTypes.AddRange(plugin.TemplateTypes);
        }
    }

    public IReadOnlyList<ITemplateTypeDescriptor> GetTemplateTypes() => _templateTypes.AsReadOnly();

    public ITemplateTypeDescriptor GetTemplateType(string key)
    {
        return _templateTypes.FirstOrDefault(t => t.Key == key);
    }

    public Type GetTemplateClassType(ITemplateTypeDescriptor descriptor)
    {
        var parts = descriptor.TemplateTypeName.Split(',');
        if (parts.Length != 2)
        {
            throw new InvalidOperationException($"Invalid template type format: {descriptor.TemplateTypeName}");
        }

        var typeName = parts[0].Trim();
        var assemblyName = parts[1].Trim();

        var assembly = AppDomain.CurrentDomain.GetAssemblies()
            .FirstOrDefault(a => a.GetName().Name == assemblyName);

        if (assembly == null)
        {
            assembly = Assembly.Load(assemblyName);
        }

        return assembly.GetType(typeName);
    }
}