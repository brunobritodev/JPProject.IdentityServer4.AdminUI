using Jp.Domain.Commands.ApiResource;

namespace Jp.Domain.Validations.ApiResource
{
    public class SaveApiScopeCommandValidation : ApiScopeValidation<SaveApiScopeCommand>
    {
        public SaveApiScopeCommandValidation()
        {
            ValidateResourceName();
        }
    }
}