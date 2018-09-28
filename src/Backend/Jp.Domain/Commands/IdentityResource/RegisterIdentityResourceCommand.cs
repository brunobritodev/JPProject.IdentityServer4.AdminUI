using Jp.Domain.Validations.IdentityResource;

namespace Jp.Domain.Commands.IdentityResource
{
    public class RegisterIdentityResourceCommand : IdentityResourceCommand
    {
        public RegisterIdentityResourceCommand()
        {
           
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterIdentityResourceCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}