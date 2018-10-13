using Jp.Domain.Commands.Role;

namespace Jp.Domain.Validations.Role
{
    public class RemoveRoleCommandValidation : RoleValidation<RemoveRoleCommand>
    {
        public RemoveRoleCommandValidation()
        {
            ValidateName();
        }
    }
}
