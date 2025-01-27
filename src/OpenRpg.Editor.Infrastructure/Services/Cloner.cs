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
            var data = Serializer.Serialize(source);
            return Deserializer.Deserialize<T>(data);
        }
    }
}