using System.Threading.Tasks;

namespace Jp.Domain.Core.Events
{
    public interface IEventStore
    {
        Task Save<T>(T theEvent) where T : Event;
    }
}