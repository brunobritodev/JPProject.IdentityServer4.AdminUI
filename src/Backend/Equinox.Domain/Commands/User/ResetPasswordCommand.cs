using Jp.Domain.Validations.User;

namespace Jp.Domain.Commands.User
{
    public class ResetPasswordCommand : UserCommand
    {
        public string Code { get; }

        public ResetPasswordCommand(string password, string confirmPassword, string code, string email)
        {
            Password = password;
            ConfirmPassword = confirmPassword;
            Code = code;
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new ResetPasswordCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}