using Jp.Domain.Commands.UserManagement;

namespace Jp.Domain.Validations.UserManagement
{
    public class UpdateProfileCommandValidation : ProfileValidation<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}
