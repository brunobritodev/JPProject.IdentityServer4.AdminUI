using Jp.Domain.Validations.IdentityResource;

namespace Jp.Domain.Commands.IdentityResource
{
    public class UpdateIdentityResourceCommand : IdentityResourceCommand
    {
        public UpdateIdentityResourceCommand(IdentityServer4.Models.IdentityResource resource)
        {
            Resource = resource;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateIdentityResourceCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
