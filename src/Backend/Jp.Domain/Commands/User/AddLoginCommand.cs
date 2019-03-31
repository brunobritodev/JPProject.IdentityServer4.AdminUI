using Jp.Domain.Validations.User;

namespace Jp.Domain.Commands.User
{
    public class AddLoginCommand : UserCommand
    {

        public AddLoginCommand(string email, string provider, string providerId)
        {
            Provider = provider;
            ProviderId = providerId;
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddLoginCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}