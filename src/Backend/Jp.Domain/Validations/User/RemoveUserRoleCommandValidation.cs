using Jp.Domain.Commands.User;

namespace Jp.Domain.Validations.User
{
    public class RemoveUserRoleCommandValidation : UserRoleValidation<RemoveUserRoleCommand>
    {
        public RemoveUserRoleCommandValidation()
        {
            ValidateUsername();
            ValidateRole();
        }
    }
}