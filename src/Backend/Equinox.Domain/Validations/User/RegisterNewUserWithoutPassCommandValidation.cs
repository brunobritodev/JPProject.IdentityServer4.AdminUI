using Equinox.Domain.Commands.User;
using FluentValidation;

namespace Equinox.Domain.Validations.User
{
    public class RegisterNewUserWithoutPassCommandValidation : UserValidation<RegisterNewUserWithoutPassCommand>
    {
        public RegisterNewUserWithoutPassCommandValidation()
        {
            ValidateName();
            ValidateUsername();
            ValidateEmail();
            ValidateProvider();
            ValidateProviderId();
        }

        private void ValidateProvider()
        {
            RuleFor(c => c.Provider)
                .NotEmpty();
        }

        private void ValidateProviderId()
        {
            RuleFor(c => c.ProviderId)
                .NotEmpty();
        }

    }
}