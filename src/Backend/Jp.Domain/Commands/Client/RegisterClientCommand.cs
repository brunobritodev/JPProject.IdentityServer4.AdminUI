using Jp.Domain.Validations.Client;

namespace Jp.Domain.Commands.Client
{
    public class RegisterClientCommand : ClientCommand
    {
        public RegisterClientCommand()
        {
           
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterClientCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}