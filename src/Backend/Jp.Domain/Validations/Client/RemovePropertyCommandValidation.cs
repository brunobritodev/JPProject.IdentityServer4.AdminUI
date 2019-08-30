using Jp.Domain.Commands.Clients;

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