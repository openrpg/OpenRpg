using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OpenRpg.Core.Variables;
using OpenRpg.Projects.Json.Convertors;

namespace OpenRpg.Projects.Json.Resolvers;

public class NoTypeForReadOnlyCollectionsResolver : DefaultContractResolver
{
    private static readonly VariablesConverter VariablesConverter = new();

    protected override JsonContract CreateContract(Type objectType)
    {
        var contract = base.CreateContract(objectType);
        if (typeof(KeyedVariables<int, object>).IsAssignableFrom(objectType))
        { contract.Converter = VariablesConverter; }
        return contract;
    }

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);
        SetNoTypeHandlingOnReadOnlyCollection(property);
        return property;
    }

    private static void SetNoTypeHandlingOnReadOnlyCollection(JsonProperty property)
    {
        var type = property.PropertyType;
        if (type is not { IsGenericType: true }) return;

        var openType = type.GetGenericTypeDefinition();
        if (openType == typeof(IReadOnlyCollection<>) || openType == typeof(IReadOnlyList<>))
        { property.TypeNameHandling = TypeNameHandling.None; }
    }
}
