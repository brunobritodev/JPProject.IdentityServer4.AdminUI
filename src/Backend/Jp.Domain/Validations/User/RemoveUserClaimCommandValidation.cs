using Jp.Domain.Commands.User;

namespace Jp.Domain.Validations.User
{
    public class RemoveUserClaimCommandValidation : UserClaimValidation<RemoveUserClaimCommand>
    {
        public RemoveUserClaimCommandValidation()
        {
            ValidateUsername();
            ValidateKey();
        }
    }
}