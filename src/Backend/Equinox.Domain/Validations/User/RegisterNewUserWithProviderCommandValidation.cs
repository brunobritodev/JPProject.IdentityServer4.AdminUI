using Jp.Domain.Commands.User;

namespace Jp.Domain.Validations.User
{
    public class RegisterNewUserWithProviderCommandValidation : UserValidation<RegisterNewUserWithProviderCommand>
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