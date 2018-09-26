using Jp.Domain.Commands.Client;

namespace Jp.Domain.Validations.Client
{
    public class RemoveSecretCommandValidation : ClientSecretValidation<RemoveSecretCommand>
    {
        public RemoveSecretCommandValidation()
        {
            ValidateClientId();
            ValidateId();
        }
    }
}