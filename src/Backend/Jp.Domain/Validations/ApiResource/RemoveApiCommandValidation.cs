using Jp.Domain.Commands.ApiResource;
using Jp.Domain.Commands.Client;
using Jp.Domain.Validations.ApiResource;

namespace Jp.Domain.Validations.Client
{
    public class RemoveApiCommandValidation : ApiSecretValidation<RemoveApiSecretCommand>
    {
        public RemoveApiCommandValidation()
        {
            ValidateResourceName();
            ValidateId();
        }
    }
}