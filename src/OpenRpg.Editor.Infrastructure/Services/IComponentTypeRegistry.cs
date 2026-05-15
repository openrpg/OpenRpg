using System;

namespace OpenRpg.Editor.Infrastructure.Services;

public interface IComponentTypeRegistry
{
    Type GetComponentType(string componentName);
    void Register(string name, Type type);
}
