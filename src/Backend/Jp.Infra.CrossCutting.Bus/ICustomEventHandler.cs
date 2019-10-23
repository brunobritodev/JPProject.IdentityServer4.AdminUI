using System.Threading.Tasks;
using Jp.Domain.Core.Events;

namespace Jp.Infra.CrossCutting.Bus
{
    public interface ICustomEventHandler
    {
        Task Handle(Event evt);
    }
}