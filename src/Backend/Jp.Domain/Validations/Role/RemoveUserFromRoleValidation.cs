using Jp.Domain.Commands.Role;

namespace Jp.Domain.Validations.Role
{
    public class RemoveUserFromRoleValidation : RoleValidation<RemoveUserFromRoleCommand>
    {
        public RemoveUserFromRoleValidation()
        {
            ValidateName();
            ValidateUsername();
        }
    }
}