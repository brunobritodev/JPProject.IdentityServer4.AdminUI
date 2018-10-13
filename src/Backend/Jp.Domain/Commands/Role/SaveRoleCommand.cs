using Jp.Domain.Validations.Role;

namespace Jp.Domain.Commands.Role
{
    public class SaveRoleCommand : RoleCommand
    {
        public SaveRoleCommand(string name)
        {
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new SaveRoleCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}