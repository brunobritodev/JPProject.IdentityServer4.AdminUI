using Equinox.Domain.Commands.UserManagement;

namespace Equinox.Domain.Validations.UserManagement
{
    public class RemoveAccountCommandValidation : ProfileValidation<RemoveAccountCommand>
    {
        public RemoveAccountCommandValidation()
        {
            ValidateId();
        }
    }
}