using Jp.Domain.Commands.IdentityResource;

namespace Jp.Domain.Validations.IdentityResource
{
    public class UpdateIdentityResourceCommandValidation : IdentityResourceValidation<UpdateIdentityResourceCommand>
    {
        public UpdateIdentityResourceCommandValidation()
        {
            ValidateName();
        }
    }
}