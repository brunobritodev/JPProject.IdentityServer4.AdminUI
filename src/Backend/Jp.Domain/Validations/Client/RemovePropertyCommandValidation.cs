using Jp.Domain.Commands.Client;

namespace Jp.Domain.Validations.Client
{
    public class RemovePropertyCommandValidation : ClientPropertyValidation<RemovePropertyCommand>
    {
        public RemovePropertyCommandValidation()
        {
            ValidateClientId();
            ValidateId();
        }
    }
}