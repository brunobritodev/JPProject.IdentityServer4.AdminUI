using Jp.Domain.Commands.IdentityResource;

namespace Jp.Domain.Validations.IdentityResource
{
    public class RegisterIdentityResourceCommandValidation : IdentityResourceValidation<RegisterIdentityResourceCommand>
    {
        public RegisterIdentityResourceCommandValidation()
        {
            ValidateName();
        }
    }
}