using Jp.Domain.Commands.User;
using Jp.Domain.Validations.User;

namespace Jp.Domain.Validations.UserManagement
{
    public class UpdateUserCommandValidation : UserManagementValidation<UpdateUserCommand>
    {
        public UpdateUserCommandValidation()
        {
            ValidateEmail();
            ValidateName();
            ValidateUsername();
        }

    }
}