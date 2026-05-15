using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenRpg.Projects.Json.Convertors;

namespace OpenRpg.Editor.Infrastructure.Services
{
    public class Cloner : ICloner
    {
        public JsonSerializerSettings SerializationSettings { get; } = new() { 
            TypeNameHandling = TypeNameHandling.Auto,
            Converters = [new VariablesConverter()],
            MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
        };
        
        public T Clone<T>(T source) where T : new()
        {
            var data = JsonConvert.SerializeObject(source, SerializationSettings);
            return JsonConvert.DeserializeObject<T>(data, SerializationSettings);
        }
        
        public object Clone(object source)
        {
            var type = source.GetType();
            var data = JsonConvert.SerializeObject(source, SerializationSettings);
            var jObject = JsonConvert.DeserializeObject<JObject>(data, SerializationSettings);
            return jObject.ToObject(type, JsonSerializer.Create(SerializationSettings));
        }
    }
}