using Jp.Domain.Commands.User;

namespace Jp.Domain.Validations.User
{
    public class RegisterNewUserCommandValidation : UserValidation<RegisterNewUserCommand>
    {
        public RegisterNewUserCommandValidation()
        {
            ValidateName();
            ValidateUsername();
            ValidateEmail();
            ValidatePassword();
        }
    }
}
