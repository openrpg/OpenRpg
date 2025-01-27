using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OpenRpg.Demos.Infrastructure.JSON;

public class IgnorePropertiesResolver : DefaultContractResolver
{
    private readonly HashSet<string> _ignoreProps;
    
    public IgnorePropertiesResolver(IEnumerable<string> propNamesToIgnore)
    {
        _ignoreProps = new HashSet<string>(propNamesToIgnore);
    }

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);
        if (_ignoreProps.Contains(property.PropertyName))
        {
            property.ShouldSerialize = _ => false;
        }
        return property;
    }
}