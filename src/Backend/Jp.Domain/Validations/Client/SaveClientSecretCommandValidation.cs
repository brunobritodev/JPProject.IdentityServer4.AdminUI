using Jp.Domain.Commands.Client;

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