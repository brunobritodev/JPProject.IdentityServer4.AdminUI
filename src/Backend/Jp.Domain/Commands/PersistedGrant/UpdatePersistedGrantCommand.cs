using Jp.Domain.Validations.PersistedGrant;

namespace Jp.Domain.Commands.PersistedGrant
{
    public class UpdatePersistedGrantCommand : PersistedGrantCommand
    {
        public UpdatePersistedGrantCommand()
        {
           
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdatePersistedGrantCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}