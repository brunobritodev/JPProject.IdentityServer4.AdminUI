using Jp.Domain.Commands.Clients;

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