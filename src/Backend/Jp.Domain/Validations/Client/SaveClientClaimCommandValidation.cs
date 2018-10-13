using Jp.Domain.Commands.Client;

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