using Jp.Domain.Commands.User;

namespace Jp.Domain.Validations.User
{
    public class RemoveUserLoginCommandValidation : UserLoginValidation<RemoveUserLoginCommand>
    {
        public RemoveUserLoginCommandValidation()
        {
            ValidateUsername();
            ValidateLoginProvider();
            ValidateProviderKey();
        }
    }
}