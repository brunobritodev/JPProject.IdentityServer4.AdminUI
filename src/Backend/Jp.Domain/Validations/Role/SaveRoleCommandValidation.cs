using Jp.Domain.Commands.Role;

namespace Jp.Domain.Validations.Role
{
    public class SaveRoleCommandValidation : RoleValidation<SaveRoleCommand>
    {
        public SaveRoleCommandValidation()
        {
            ValidateName();
        }
    }
}