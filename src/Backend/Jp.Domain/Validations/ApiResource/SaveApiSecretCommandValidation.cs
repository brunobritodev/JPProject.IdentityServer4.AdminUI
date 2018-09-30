using Jp.Domain.Commands.ApiResource;
using Jp.Domain.Validations.Client;

namespace Jp.Domain.Validations.ApiResource
{
    public class SaveApiSecretCommandValidation : ApiSecretValidation<SaveApiSecretCommand>
    {
        public SaveApiSecretCommandValidation()
        {
            ValidateClientId();
            ValidateType();
            ValidateValue();
            ValidateHashType();
        }
    }
}