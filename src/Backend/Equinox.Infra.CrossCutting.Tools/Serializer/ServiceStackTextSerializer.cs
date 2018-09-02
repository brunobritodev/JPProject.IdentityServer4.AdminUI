using Jp.Domain.Interfaces;

namespace Jp.Infra.CrossCutting.Tools.Serializer
{
    public class ServiceStackTextSerializer : ISerializer
    {
        public T DeserializeFromString<T>(string value)
        {
            return ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(value);
        }

        public string SerializeToString<T>(T value)
        {
            return ServiceStack.Text.JsonSerializer.SerializeToString(value);
        }
    }
}
