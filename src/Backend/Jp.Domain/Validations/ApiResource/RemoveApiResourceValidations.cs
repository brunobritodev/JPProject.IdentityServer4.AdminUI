using Jp.Domain.Commands.ApiResource;

namespace Jp.Domain.Validations.ApiResource
{
    public class RemoveApiResourceCommandValidation : ApiResourceValidation<RemoveApiResourceCommand>
    {
        public RemoveApiResourceCommandValidation()
        {
            ValidateName();
        }
    }
}