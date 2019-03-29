using System;
using Jp.Domain.Validations.UserManagement;

namespace Jp.Domain.Commands.UserManagement
{
    public class UpdateProfilePictureCommand : ProfileCommand
    {
        public UpdateProfilePictureCommand(Guid? id, string picture)
        {
            Picture = picture;
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateProfilePictureCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}