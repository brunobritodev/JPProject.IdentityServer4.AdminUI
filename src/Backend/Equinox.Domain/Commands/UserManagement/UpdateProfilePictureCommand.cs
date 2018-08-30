using System;
using Equinox.Domain.Validations.UserManagement;

namespace Equinox.Domain.Commands.UserManagement
{
    public class UpdateProfilePictureCommand : ProfileCommand
    {
        public UpdateProfilePictureCommand(Guid? id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateProfilePictureCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}