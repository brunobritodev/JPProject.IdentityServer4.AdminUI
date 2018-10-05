using Jp.Domain.Commands.User;

namespace Jp.Domain.Validations.User
{
    public class SaveUserRoleCommandValidation : UserRoleValidation<SaveUserRoleCommand>
    {
        public SaveUserRoleCommandValidation()
        {
            ValidateUsername();
            ValidateRole();
        }
    }
}