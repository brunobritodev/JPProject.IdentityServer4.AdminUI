using Equinox.Domain.Commands.User;

namespace Equinox.Domain.Validations.User
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
