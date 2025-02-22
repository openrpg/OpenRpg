using Newtonsoft.Json;
using Persistity.Core.Serialization;
using Persistity.Extensions;

namespace OpenRpg.Editor.Infrastructure.Services
{
    public class Cloner : ICloner
    {
        public ISerializer Serializer { get; }
        public IDeserializer Deserializer { get; }

        public Cloner(ISerializer serializer, IDeserializer deserializer)
        {
            Serializer = serializer;
            Deserializer = deserializer;
        }

        public T Clone<T>(T source) where T : new()
        {
            var serializationSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto };
            var data = Serializer.Serialize(source, serializationSettings);
            return Deserializer.Deserialize<T>(data, serializationSettings);
        }
    }
}