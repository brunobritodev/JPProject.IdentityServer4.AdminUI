using Jp.Domain.Commands.UserManagement;

namespace Jp.Domain.Validations.UserManagement
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