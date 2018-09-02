    using Jp.Domain.Commands.UserManagement;

namespace Jp.Domain.Validations.UserManagement
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