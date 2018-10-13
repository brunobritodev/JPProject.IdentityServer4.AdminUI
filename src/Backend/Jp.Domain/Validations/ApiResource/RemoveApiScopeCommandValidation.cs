using Jp.Domain.Commands.ApiResource;

namespace Jp.Domain.Validations.ApiResource
{
    public class RemoveApiScopeCommandValidation : ApiScopeValidation<RemoveApiScopeCommand>
    {
        public RemoveApiScopeCommandValidation()
        {
            ValidateResourceName();
            ValidateId();
        }
    }
}