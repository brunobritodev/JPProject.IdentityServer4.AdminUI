using Equinox.Domain.Commands.UserManagement;

namespace Equinox.Domain.Validations.UserManagement
{
    public class UpdateProfilePictureCommandValidation : ProfileValidation<UpdateProfilePictureCommand>
    {
        public UpdateProfilePictureCommandValidation()
        {
            ValidatePicture();
        }
    }
}