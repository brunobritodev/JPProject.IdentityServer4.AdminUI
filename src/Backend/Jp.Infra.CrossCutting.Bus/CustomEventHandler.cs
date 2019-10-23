using System.Threading.Tasks;
using Jp.Domain.Core.Events;
using Microsoft.Extensions.Logging;

namespace Jp.Infra.CrossCutting.Bus
{
    public class CustomEventHandler : ICustomEventHandler
    {
        private readonly ILogger<CustomEventHandler> _logger;

        public CustomEventHandler(ILogger<CustomEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(Event evt)
        {
            _logger.LogInformation("{@event}", evt);

            return Task.CompletedTask;
        }
    }
}