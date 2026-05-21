using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenRpg.Core.Variables;

namespace OpenRpg.Projects.Json.Convertors;

public class VariablesConverter : JsonConverter
{
    private static readonly Dictionary<int, Type> _collectionElementTypes = new();

    public static void RegisterKey(int key, Type elementType)
    {
        _collectionElementTypes[key] = elementType;
    }

    public static void Initialize(IDictionary<int, Type> collectionElementTypes)
    {
        foreach (var kvp in collectionElementTypes)
        { _collectionElementTypes[kvp.Key] = kvp.Value; }
    }

    public override bool CanConvert(Type objectType)
    {
        return typeof(KeyedVariables<int, object>).IsAssignableFrom(objectType);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var obj = JObject.Load(reader);

        var dict = obj.Properties()
            .ToDictionary(
                p => int.Parse(p.Name),
                p => ReadToken(p.Value, serializer, int.Parse(p.Name)));

        var instance = existingValue as KeyedVariables<int, object>
                       ?? Activator.CreateInstance(objectType, true) as KeyedVariables<int, object>;

        if (instance == null)
        { return new KeyedVariables<int, object> { InternalVariables = dict }; }

        instance.InternalVariables = dict;
        return instance;
    }

    private object? ReadToken(JToken token, JsonSerializer serializer, int? currentKey = null)
    {
        switch (token.Type)
        {
            case JTokenType.Object:
            {
                var jobject = (JObject)token;

                if (jobject.Property("$type") != null)
                {
                    var typeName = jobject["$type"]!.Value<string>()!;
                    var resolvedType = Type.GetType(typeName, throwOnError: true);

                    using var subReader = jobject.CreateReader();
                    return serializer.Deserialize(subReader, resolvedType!);
                }

                if (currentKey.HasValue && _collectionElementTypes.TryGetValue(currentKey.Value, out var elementType))
                {
                    using var subReader = jobject.CreateReader();
                    return serializer.Deserialize(subReader, elementType);
                }

                return jobject.Properties()
                    .ToDictionary(
                        p => p.Name,
                        p => ReadToken(p.Value, serializer));
            }

            case JTokenType.Array:
            {
                if (currentKey.HasValue && _collectionElementTypes.TryGetValue(currentKey.Value, out var elementType))
                {
                    var listType = typeof(List<>).MakeGenericType(elementType);
                    var list = (IList)Activator.CreateInstance(listType)!;

                    foreach (var child in token.Children())
                    {
                        if (child is JObject jo && jo.Property("$type") != null)
                        {
                            var typeName = jo["$type"]!.Value<string>()!;
                            var resolvedType = Type.GetType(typeName, throwOnError: true);
                            using var subReader = jo.CreateReader();
                            list.Add(serializer.Deserialize(subReader, resolvedType!));
                        }
                        else
                        {
                            list.Add(child.ToObject(elementType, serializer));
                        }
                    }

                    return list;
                }

                return token.Children()
                    .Select(t => ReadToken(t, serializer))
                    .ToList();
            }

            case JTokenType.Integer:
                return token.Value<long>();

            case JTokenType.Float:
                return token.Value<double>();

            case JTokenType.String:
                return token.Value<string>();

            case JTokenType.Boolean:
                return token.Value<bool>();

            case JTokenType.Null:
            case JTokenType.Undefined:
                return null;

            case JTokenType.Date:
                return token.Value<DateTime>();

            default:
                return ((JValue)token).Value;
        }
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        var variables = value as KeyedVariables<int, object>;
        if (variables == null)
        {
            writer.WriteNull();
            return;
        }

        writer.WriteStartObject();
        if (variables.InternalVariables == null)
        {
            writer.WriteEndObject();
            return;
        }

        foreach (var kvp in variables.InternalVariables)
        {
            writer.WritePropertyName(kvp.Key.ToString());

            if (kvp.Value is IList list && _collectionElementTypes.TryGetValue(kvp.Key, out var elementType))
            {
                writer.WriteStartArray();
                foreach (var item in list)
                {
                    if (item == null)
                    { serializer.Serialize(writer, null); }
                    else if (elementType.IsInterface || elementType.IsAbstract)
                    {
                        var typeName = $"{item.GetType().FullName}, {item.GetType().Assembly.GetName().Name}";
                        var content = JObject.FromObject(item);
                        writer.WriteStartObject();
                        writer.WritePropertyName("$type");
                        writer.WriteValue(typeName);
                        foreach (var prop in content.Properties())
                        { prop.WriteTo(writer); }
                        writer.WriteEndObject();
                    }
                    else
                    { serializer.Serialize(writer, item, item.GetType()); }
                }
                writer.WriteEndArray();
            }
            else
            {
                serializer.Serialize(writer, kvp.Value);
            }
        }

        writer.WriteEndObject();
    }
}
