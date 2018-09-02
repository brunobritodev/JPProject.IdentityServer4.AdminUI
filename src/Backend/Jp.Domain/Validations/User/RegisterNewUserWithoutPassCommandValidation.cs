using Jp.Domain.Commands.User;

namespace Jp.Domain.Validations.User
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

        

    }
}