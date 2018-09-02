using Jp.Domain.Validations.User;

namespace Jp.Domain.Commands.User
{
    public class SendResetLinkCommand: UserCommand
    {
        public SendResetLinkCommand(string email, string username)
        {
            Email = email;
            Username = username;
        }

        public override bool IsValid()
        {
            ValidationResult = new SendResetLinkCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}