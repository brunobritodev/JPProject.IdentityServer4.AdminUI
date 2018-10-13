using Jp.Domain.Commands.Client;
using Jp.Domain.Commands.User;

namespace Jp.Domain.Validations.User
{
    public class SaveUserClaimCommandValidation : UserClaimValidation<SaveUserClaimCommand>
    {
        public SaveUserClaimCommandValidation()
        {
            ValidateUsername();
            ValidateKey();
            ValidateValue();
        }
    }
}