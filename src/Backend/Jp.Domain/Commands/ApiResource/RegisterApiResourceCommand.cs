using Jp.Domain.Validations.ApiResource;

namespace Jp.Domain.Commands.ApiResource
{
    public class RegisterApiResourceCommand : ApiResourceCommand
    {
        public RegisterApiResourceCommand()
        {
           
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterApiResourceCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}