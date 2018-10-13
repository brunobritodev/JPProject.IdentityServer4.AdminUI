using Jp.Domain.Commands.Role;

namespace Jp.Domain.Validations.Role
{
    public class UpdateRoleCommandValidation : RoleValidation<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidation()
        {
            ValidateName();
            ValidateNewName();
        }
    }
}