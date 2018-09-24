using Jp.Domain.Validations.IdentityResources;

namespace Jp.Domain.Commands.IdentityResources
{
    public class RegisterIdentityResourcesCommand : IdentityResourcesCommand
    {
        public RegisterIdentityResourcesCommand()
        {
           
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterIdentityResourcesCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}