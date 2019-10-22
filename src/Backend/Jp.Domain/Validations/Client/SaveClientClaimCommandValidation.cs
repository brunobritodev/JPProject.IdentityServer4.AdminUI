using Jp.Domain.Commands.Clients;

namespace Jp.Domain.Validations.Client
{
    public class SaveClientClaimCommandValidation : ClientClaimValidation<SaveClientClaimCommand>
    {
        public SaveClientClaimCommandValidation()
        {
            ValidateClientId();
            ValidateKey();
            ValidateValue();
        }
    }
}