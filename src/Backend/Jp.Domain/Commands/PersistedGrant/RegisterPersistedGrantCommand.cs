using Jp.Domain.Validations.PersistedGrant;

namespace Jp.Domain.Commands.PersistedGrant
{
    public class RegisterPersistedGrantCommand : PersistedGrantCommand
    {
        public RegisterPersistedGrantCommand()
        {
           
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterPersistedGrantCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}