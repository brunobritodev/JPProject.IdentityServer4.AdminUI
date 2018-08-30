using Equinox.Domain.Commands.UserManagement;

namespace Equinox.Domain.Validations.UserManagement
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
