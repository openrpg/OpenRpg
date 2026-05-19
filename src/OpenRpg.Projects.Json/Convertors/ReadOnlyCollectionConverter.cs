using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using OpenRpg.Core.Variables;

namespace OpenRpg.Projects.Json.Convertors;

public class ReadOnlyCollectionConverter : JsonConverter
{
    public override bool CanRead => false;

    public override bool CanConvert(Type objectType)
    {
        if (!objectType.IsGenericType)
            return false;

        var openType = objectType.GetGenericTypeDefinition();
        return openType == typeof(IReadOnlyCollection<>)
            || openType == typeof(IReadOnlyList<>);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        => throw new NotSupportedException("ReadOnlyCollectionConverter is write-only");

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value is IEnumerable enumerable)
        {
            writer.WriteStartArray();
            foreach (var item in enumerable)
            {
                if (item != null)
                { serializer.Serialize(writer, item, item.GetType()); }
                else
                { serializer.Serialize(writer, null); }
            }
            writer.WriteEndArray();
        }
        else
        {
            writer.WriteStartArray();
            writer.WriteEndArray();
        }
    }
}
