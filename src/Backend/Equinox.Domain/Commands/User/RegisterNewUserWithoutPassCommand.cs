using Equinox.Domain.Validations.User;

namespace Equinox.Domain.Commands.User
{
    public class RegisterNewUserWithoutPassCommand : UserCommand
    {
        public string Provider { get; }
        public string ProviderId { get; }

        public RegisterNewUserWithoutPassCommand(string username, string email, string name, string picture, string provider, string providerId)
        {
            Provider = provider;
            ProviderId = providerId;
            Username = username;
            Picture = picture;
            Email = email;
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewUserWithoutPassCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}