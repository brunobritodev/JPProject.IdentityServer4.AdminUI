using Jp.Domain.Validations.User;

namespace Jp.Domain.Commands.User
{
    public class ConfirmEmailCommand : UserCommand
    {
        public string Code { get; }

        public ConfirmEmailCommand(string code, string email)
        {
            Code = code;
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new ConfirmEmailCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}