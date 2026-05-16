using System;
using System.Reflection;

namespace OpenRpg.Editor.Infrastructure.Extensions;

public static class TemplatePropertyWriter
{
    public static void SetValue(PropertyInfo property, object target, object newValue)
    {
        var propType = property.PropertyType;
        if (propType == typeof(int))
        {
            property.SetValue(target, int.TryParse(newValue?.ToString(), out var result) ? result : 0);
        }
        else if (propType == typeof(float))
        {
            property.SetValue(target, float.TryParse(newValue?.ToString(), out var fResult) ? fResult : 0f);
        }
        else if (propType == typeof(double))
        {
            property.SetValue(target, double.TryParse(newValue?.ToString(), out var dResult) ? dResult : 0d);
        }
        else if (propType == typeof(bool))
        {
            property.SetValue(target, newValue);
        }
        else
        {
            property.SetValue(target, newValue);
        }
    }
}
