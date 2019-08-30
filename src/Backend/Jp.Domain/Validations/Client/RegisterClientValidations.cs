using Jp.Domain.Commands.Clients;

namespace Jp.Domain.Validations.Client
{
    public class RemoveClientCommandValidation : ClientValidation<RemoveClientCommand>
    {
        public RemoveClientCommandValidation()
        {
            ValidateClientId();
        }
    }
}