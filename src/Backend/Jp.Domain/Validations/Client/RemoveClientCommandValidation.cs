using Jp.Domain.Commands.Clients;

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