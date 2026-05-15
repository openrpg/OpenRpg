using System;
using System.Collections.Generic;
using System.Reflection;
using OpenRpg.Core.Templates.Variables;

namespace OpenRpg.Editor.Infrastructure.Services;

public interface IEditorPropertyResolver
{
    Dictionary<string, PropertyInfo> BuildPropertyMap(Type templateType);
    string GetEditorType(PropertyInfo prop);
    Type GetCollectionElementType(PropertyInfo property);
    string GetCollectionParamName(string propertyName, Type componentType);
    object GetNestedVariableContainer(ITemplateVariables variables, string containerName);
    object GetOrCreateNestedContainer(ITemplateVariables variables, string containerName);
}
