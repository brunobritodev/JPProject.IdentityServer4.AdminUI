using Jp.Domain.Commands.Client;

namespace Jp.Domain.Validations.Client
{
    public class RemoveClientClaimCommandValidation : ClientClaimValidation<RemoveClientClaimCommand>
    {
        public RemoveClientClaimCommandValidation()
        {
            ValidateClientId();
            ValidateId();
        }
    }
}