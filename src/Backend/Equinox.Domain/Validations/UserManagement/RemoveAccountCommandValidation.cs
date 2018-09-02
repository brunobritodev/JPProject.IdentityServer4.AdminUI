using Jp.Domain.Commands.UserManagement;

namespace Jp.Domain.Validations.UserManagement
{
    public class RemoveAccountCommandValidation : ProfileValidation<RemoveAccountCommand>
    {
        public RemoveAccountCommandValidation()
        {
            ValidateId();
        }
    }
}