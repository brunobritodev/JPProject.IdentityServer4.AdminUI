using Jp.Domain.Commands.ApiResource;
using Jp.Domain.Commands.Client;
using Jp.Domain.Validations.Client;

namespace Jp.Domain.Validations.ApiResource
{
    public class RemoveApiSecretCommandValidation : ApiSecretValidation<RemoveApiSecretCommand>
    {
        public RemoveApiSecretCommandValidation()
        {
            ValidateResourceName();
            ValidateId();
        }
    }
}