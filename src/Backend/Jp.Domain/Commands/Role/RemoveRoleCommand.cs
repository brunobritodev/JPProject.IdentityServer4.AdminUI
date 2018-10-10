using Jp.Domain.Validations.Role;

namespace Jp.Domain.Commands.Role
{
    public class RemoveRoleCommand : RoleCommand
    {
        public RemoveRoleCommand(string name)
        {
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveRoleCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
