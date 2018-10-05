using Jp.Domain.Validations.User;

namespace Jp.Domain.Commands.User
{
    public class SaveUserRoleCommand : UserRoleCommand
    {

        public SaveUserRoleCommand(string username, string role)
        {
            Role = role;

            Username = username;
        }
        public override bool IsValid()
        {
            ValidationResult = new SaveUserRoleCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}