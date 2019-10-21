using IdentityServer4.Events;
using IdentityServer4.Services;
using Jp.Domain.Core.Events;
using Jp.Domain.Interfaces;
using System.Threading.Tasks;
using EventTypes = Jp.Domain.Core.Events.EventTypes;
using Is4Event = IdentityServer4.Events.Event;

namespace Jp.UI.SSO.Configuration
{
    public class IdentityServerEventStore : IEventSink
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly ISystemUser _user;

        public IdentityServerEventStore(IEventStoreRepository eventStoreRepository, ISystemUser user)
        {
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }
        public Task PersistAsync(Is4Event evt)
        {
            var es = new StoredEvent(
                evt.Category,
                (EventTypes)(int)evt.EventType,
                evt.Name,
                evt.LocalIpAddress,
                evt.RemoteIpAddress,
                    evt.ToString()
                ).ReplaceTimeStamp(evt.TimeStamp);

            if (_user.IsAuthenticated())
                es.SetUser(_user.Username);

            switch (evt)
            {
                case ApiAuthenticationFailureEvent apiAuthenticationFailureEvent:
                    es.SetAggregate(apiAuthenticationFailureEvent.ApiName);
                    break;
                case ApiAuthenticationSuccessEvent apiAuthenticationSuccessEvent:
                    es.SetAggregate(apiAuthenticationSuccessEvent.ApiName);
                    break;
                case ClientAuthenticationFailureEvent clientAuthenticationFailureEvent:
                    es.SetAggregate(clientAuthenticationFailureEvent.ClientId);
                    break;
                case ClientAuthenticationSuccessEvent clientAuthenticationSuccessEvent:
                    es.SetAggregate(clientAuthenticationSuccessEvent.ClientId);
                    break;
                case ConsentDeniedEvent consentDeniedEvent:
                    es.SetAggregate(consentDeniedEvent.ClientId);
                    break;
                case ConsentGrantedEvent consentGrantedEvent:
                    es.SetAggregate(consentGrantedEvent.ClientId);
                    break;
                case DeviceAuthorizationFailureEvent deviceAuthorizationFailureEvent:
                    es.SetAggregate(deviceAuthorizationFailureEvent.ClientId);
                    break;
                case DeviceAuthorizationSuccessEvent deviceAuthorizationSuccessEvent:
                    es.SetAggregate(deviceAuthorizationSuccessEvent.ClientId);
                    break;
                case GrantsRevokedEvent grantsRevokedEvent:
                    es.SetAggregate(grantsRevokedEvent.ClientId);
                    break;
                case InvalidClientConfigurationEvent invalidClientConfigurationEvent:
                    es.SetAggregate(invalidClientConfigurationEvent.ClientId);
                    break;
                case TokenIntrospectionFailureEvent tokenIntrospectionFailureEvent:
                    es.SetAggregate(tokenIntrospectionFailureEvent.ApiName);
                    break;
                case TokenIntrospectionSuccessEvent tokenIntrospectionSuccessEvent:
                    es.SetAggregate(tokenIntrospectionSuccessEvent.ApiName);
                    break;
                case TokenIssuedFailureEvent tokenIssuedFailureEvent:
                    es.SetAggregate(tokenIssuedFailureEvent.ClientId);
                    break;
                case TokenIssuedSuccessEvent tokenIssuedSuccessEvent:
                    es.SetAggregate(tokenIssuedSuccessEvent.ClientId);
                    break;
                case TokenRevokedSuccessEvent tokenRevokedSuccessEvent:
                    es.SetAggregate(tokenRevokedSuccessEvent.ClientId);
                    break;
                case UnhandledExceptionEvent unhandledExceptionEvent:
                    break;
                case UserLoginFailureEvent userLoginFailureEvent:
                    es.SetUser(userLoginFailureEvent.Username).SetAggregate(userLoginFailureEvent.ClientId);
                    break;
                case UserLoginSuccessEvent userLoginSuccessEvent:
                    es.SetUser(userLoginSuccessEvent.Username).SetAggregate(userLoginSuccessEvent.ClientId);
                    break;
                case UserLogoutSuccessEvent userLogoutSuccessEvent:
                    es.SetAggregate(userLogoutSuccessEvent.SubjectId);
                    break;
            }

            return _eventStoreRepository.Store(es);
        }

    }
}
