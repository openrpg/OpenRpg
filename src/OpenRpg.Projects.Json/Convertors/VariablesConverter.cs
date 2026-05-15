using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OpenRpg.Projects.Json.Convertors;

public class VariablesConverter
    : JsonConverter<Dictionary<int, object?>>
{
    public override Dictionary<int, object?>? ReadJson(
        JsonReader reader,
        Type objectType,
        Dictionary<int, object?>? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        var obj = JObject.Load(reader);

        return obj.Properties()
            .ToDictionary(
                p => int.Parse(p.Name),
                p => ReadToken(p.Value, serializer)
            );
    }

    private object? ReadToken(
        JToken token,
        JsonSerializer serializer)
    {
        switch (token.Type)
        {
            case JTokenType.Object:
            {
                var jobject = (JObject)token;
                
                if (jobject.Property("$type") != null)
                {
                    var typeName =
                        jobject["$type"]!.Value<string>()!;

                    var resolvedType = Type.GetType(
                        typeName,
                        throwOnError: true);

                    using var subReader =
                        jobject.CreateReader();

                    return serializer.Deserialize(
                        subReader,
                        resolvedType!);
                }

                // Plain object without $type
                return jobject.Properties()
                    .ToDictionary(
                        p => p.Name,
                        p => ReadToken(p.Value, serializer)
                    );
            }

            case JTokenType.Array:
            {
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

    public override void WriteJson(
        JsonWriter writer,
        Dictionary<int, object?>? value,
        JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}