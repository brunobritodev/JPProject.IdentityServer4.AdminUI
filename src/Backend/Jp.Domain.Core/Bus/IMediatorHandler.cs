using System.Threading.Tasks;
using Jp.Domain.Core.Commands;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task<bool> SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
