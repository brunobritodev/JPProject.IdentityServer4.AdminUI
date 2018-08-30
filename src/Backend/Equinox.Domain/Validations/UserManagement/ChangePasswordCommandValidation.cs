using Equinox.Domain.Commands.UserManagement;

namespace Equinox.Domain.Validations.UserManagement
{
    public class ChangePasswordCommandValidation : PasswordCommandValidation<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidation()
        {
            ValidateId();
            ValidateOldPassword();
            ValidatePassword();
        }
    }
}