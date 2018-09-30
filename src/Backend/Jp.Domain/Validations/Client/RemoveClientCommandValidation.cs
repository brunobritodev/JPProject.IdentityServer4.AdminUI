using Jp.Domain.Commands.Client;

namespace Jp.Domain.Validations.Client
{
    public class RemoveClientSecretCommandValidation : ClientSecretValidation<RemoveClientSecretCommand>
    {
        public RemoveClientSecretCommandValidation()
        {
            ValidateClientId();
            ValidateId();
        }
    }
}