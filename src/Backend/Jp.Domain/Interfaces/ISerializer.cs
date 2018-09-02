namespace Jp.Domain.Interfaces
{
    public interface ISerializer
    {
        T DeserializeFromString<T>(string value);
    }
}
