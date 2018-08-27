using Equinox.Domain.Commands.User;

namespace Equinox.Domain.Validations.User
{
    public class RegisterNewUserWithProviderCommandValidation : UserValidation<RegisterNewUserWithProvider>
    {
        public RegisterNewUserWithProviderCommandValidation()
        {
            ValidateName();
            ValidateUsername();
            ValidateEmail();
            ValidateProvider();
            ValidateProviderId();
        }
    }
}