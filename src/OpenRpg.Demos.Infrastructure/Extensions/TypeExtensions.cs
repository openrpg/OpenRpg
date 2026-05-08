using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenRpg.Demos.Infrastructure.Extensions;

public static class TypeExtensions
{
    public static Dictionary<int, string> GetTypeFieldsDictionary(this Type typeObject, bool ignoreZero = true)
    {
        var relatedInterfaceTypes = typeObject.GetInterfaces().ToList();
        relatedInterfaceTypes.Add(typeObject);
            
        var results = relatedInterfaceTypes
            .SelectMany(x => x.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            .ToDictionary(x => (int)x.GetValue(null), x => x.Name.UnPascalCase());
        
        if(ignoreZero) { results.Remove(0); }

        return results;
    }
}