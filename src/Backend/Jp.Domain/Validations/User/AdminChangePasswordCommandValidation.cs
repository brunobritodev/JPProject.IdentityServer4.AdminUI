using Jp.Domain.Commands.User;

namespace Jp.Domain.Validations.User
{
    public class AdminChangePasswordCommandValidation : UserValidation<AdminChangePasswordCommand>
    {
        public AdminChangePasswordCommandValidation()
        {
            ValidateUsername();
        }
    }
}