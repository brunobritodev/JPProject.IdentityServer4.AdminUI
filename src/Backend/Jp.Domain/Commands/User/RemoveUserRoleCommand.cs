using Jp.Domain.Validations.User;

namespace Jp.Domain.Commands.User
{
    public class RemoveUserRoleCommand : UserRoleCommand
    {

        public RemoveUserRoleCommand(string username, string role)
        {
            Role = role;
            Username = username;
        }
        public override bool IsValid()
        {
            ValidationResult = new RemoveUserRoleCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}