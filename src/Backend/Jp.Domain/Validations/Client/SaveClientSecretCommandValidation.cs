using Jp.Domain.Commands.Clients;

namespace Jp.Domain.Validations.Client
{
    public class SaveClientSecretCommandValidation : ClientSecretValidation<SaveClientSecretCommand>
    {
        public SaveClientSecretCommandValidation()
        {
            ValidateClientId();
            ValidateType();
            ValidateValue();
            ValidateHashType();
        }
    }
}