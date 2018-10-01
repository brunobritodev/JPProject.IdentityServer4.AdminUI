using Jp.Domain.Validations.PersistedGrant;

namespace Jp.Domain.Commands.PersistedGrant
{
    public class RemovePersistedGrantCommand : PersistedGrantCommand
    {

        public RemovePersistedGrantCommand(string key)
        {
            Key = key;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemovePersistedGrantCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}