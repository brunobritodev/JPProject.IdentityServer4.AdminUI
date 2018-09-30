using Jp.Domain.Commands.ApiResource;
using Jp.Domain.Commands.Client;

namespace Jp.Domain.Validations.Client
{
    public class RemoveApiCommandValidation : ApiSecretValidation<RemoveApiSecretCommand>
    {
        public RemoveApiCommandValidation()
        {
            ValidateClientId();
            ValidateId();
        }
    }
}