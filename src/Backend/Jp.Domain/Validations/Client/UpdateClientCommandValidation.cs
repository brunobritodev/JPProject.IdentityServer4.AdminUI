using Jp.Domain.Commands.Clients;

namespace Jp.Domain.Validations.Client
{
    public class UpdateClientCommandValidation : ClientValidation<UpdateClientCommand>
    {
        public UpdateClientCommandValidation()
        {
            ValidateGrantType();
            ValidateOldClientId();
            ValidateIdentityTokenLifetime();
            ValidateAccessTokenLifetime();
            ValidateAuthorizationCodeLifetime();
            ValidateSlidingRefreshTokenLifetime();
            ValidateDeviceCodeLifetime();
            ValidateAbsoluteRefreshTokenLifetime();
        }
    }
}