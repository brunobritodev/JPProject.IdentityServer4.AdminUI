using Equinox.Domain.Validations.User;

namespace Equinox.Domain.Commands.User
{
    public class RegisterNewUserWithProvider : UserCommand
    {

        public RegisterNewUserWithProvider(string username, string email, string name, string phoneNumber, string password, string confirmPassword, string picture, string provider, string providerId)
        {
            Picture = picture;
            Provider = provider;
            ProviderId = providerId;
            Username = username;
            Email = email;
            Name = name;
            PhoneNumber = phoneNumber;
            Password = password;
            ConfirmPassword = confirmPassword;

        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewUserWithProviderCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}