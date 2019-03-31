using Jp.Domain.Commands.User;

namespace Jp.Domain.Validations.User
{
    public class AddLoginCommandValidation : UserValidation<AddLoginCommand>
    {
        public AddLoginCommandValidation()
        {
            ValidateEmail();
            ValidateProvider();
            ValidateProviderId();
        }

    }
}