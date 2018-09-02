using Jp.Domain.Commands.UserManagement;

namespace Jp.Domain.Validations.UserManagement
{
    public class UpdateProfilePictureCommandValidation : ProfileValidation<UpdateProfilePictureCommand>
    {
        public UpdateProfilePictureCommandValidation()
        {
            ValidatePicture();
        }
    }
}