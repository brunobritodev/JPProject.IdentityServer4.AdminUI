using Jp.Domain.Commands.IdentityResource;

namespace Jp.Domain.Validations.IdentityResource
{
    public class RemoveIdentityResourceCommandValidation : IdentityResourceValidation<RemoveIdentityResourceCommand>
    {
        public RemoveIdentityResourceCommandValidation()
        {
            ValidateName();
        }
    }
}