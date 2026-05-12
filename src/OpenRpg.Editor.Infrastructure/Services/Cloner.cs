using Newtonsoft.Json;

namespace OpenRpg.Editor.Infrastructure.Services
{
    public class Cloner : ICloner
    {
        public T Clone<T>(T source) where T : new()
        {
            var serializationSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto };
            var data = JsonConvert.SerializeObject(source, serializationSettings);
            return JsonConvert.DeserializeObject<T>(data, serializationSettings);
        }
    }
}