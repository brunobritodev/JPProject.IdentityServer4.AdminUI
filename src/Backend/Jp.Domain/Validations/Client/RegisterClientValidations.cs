using Jp.Domain.Commands.Client;

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