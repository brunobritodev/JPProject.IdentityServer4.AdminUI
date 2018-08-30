    using Equinox.Domain.Commands.UserManagement;

namespace Equinox.Domain.Validations.UserManagement
{
    public class SetPasswordCommandValidation : PasswordCommandValidation<SetPasswordCommand>
    {
        public SetPasswordCommandValidation()
        {
            ValidateId();
            ValidatePassword();
        }
    }
}