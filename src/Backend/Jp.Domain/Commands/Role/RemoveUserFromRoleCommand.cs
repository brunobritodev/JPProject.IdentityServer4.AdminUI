using Jp.Domain.Validations.Role;

namespace Jp.Domain.Commands.Role
{
    public class RemoveUserFromRoleCommand : RoleCommand
    {

        public RemoveUserFromRoleCommand(string roleName, string username)
        {
            Username = username;
            Name = roleName;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveUserFromRoleValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}