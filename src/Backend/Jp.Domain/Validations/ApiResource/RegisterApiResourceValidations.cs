using Jp.Domain.Commands.ApiResource;

namespace Jp.Domain.Validations.ApiResource
{
    public class RegisterApiResourceCommandValidation : ApiResourceValidation<RegisterApiResourceCommand>
    {
        public RegisterApiResourceCommandValidation()
        {
            ValidateName();
        }
    }
}