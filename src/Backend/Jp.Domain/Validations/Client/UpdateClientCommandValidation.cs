using Jp.Domain.Commands.Client;

namespace Jp.Domain.Validations.Client
{
    public class UpdateClientCommandValidation : ClientValidation<UpdateClientCommand>
    {
        public UpdateClientCommandValidation()
        {
            ValidateGrantType();
            ValidateOldClientId();
        }
    }
}