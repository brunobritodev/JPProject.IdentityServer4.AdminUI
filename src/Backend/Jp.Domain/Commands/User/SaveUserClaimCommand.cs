using Jp.Domain.Validations.User;

namespace Jp.Domain.Commands.User
{
    public class SaveUserClaimCommand : UserClaimCommand
    {
        public SaveUserClaimCommand(string username, string type, string value)
        {
            Type = type;
            Username = username;
            Value = value;
        }
        public override bool IsValid()
        {
            ValidationResult = new SaveUserClaimCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}