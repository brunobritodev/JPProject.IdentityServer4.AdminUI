using System.Threading;
using System.Threading.Tasks;
using Jp.Domain.Commands.ApiResource;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Interfaces;
using MediatR;

namespace Jp.Domain.CommandHandlers
{
    public class ApiResourceCommandHandler : CommandHandler,
        IRequestHandler<RegisterApiResourceCommand>
    {
        private readonly IApiResourceRepository _apiResourceRepository;

        public ApiResourceCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications,
            IApiResourceRepository apiResourceService) : base(uow, bus, notifications)
        {
            _apiResourceRepository = apiResourceService;
        }


        public Task Handle(RegisterApiResourceCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.CompletedTask;
            }

            // Businness logic here

            //if (Commit())
            //{
            //    Bus.RaiseEvent(new ApiResourceRegisteredEvent(ApiResource.Id));
            //}

            return Task.CompletedTask;
        }

    }
}