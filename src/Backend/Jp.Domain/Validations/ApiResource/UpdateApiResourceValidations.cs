using Jp.Domain.Commands.ApiResource;

namespace Jp.Domain.Validations.ApiResource
{
    public class UpdateApiResourceCommandValidation : ApiResourceValidation<UpdateApiResourceCommand>
    {
        public UpdateApiResourceCommandValidation()
        {
            ValidateName();
        }
    }
}