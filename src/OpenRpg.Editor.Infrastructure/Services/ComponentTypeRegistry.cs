using System;
using System.Collections.Generic;

namespace OpenRpg.Editor.Infrastructure.Services;

public class ComponentTypeRegistry : IComponentTypeRegistry
{
    private readonly Dictionary<string, Type> _components = new();

    public void Register(string name, Type type)
    {
        _components[name] = type;
    }

    public Type GetComponentType(string componentName)
    {
        return _components.TryGetValue(componentName, out var type) ? type : null;
    }
}
