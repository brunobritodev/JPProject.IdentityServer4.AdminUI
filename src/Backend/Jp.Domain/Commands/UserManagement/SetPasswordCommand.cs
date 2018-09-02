using System;
using Jp.Domain.Validations.UserManagement;

namespace Jp.Domain.Commands.UserManagement
{
    public class SetPasswordCommand : PasswordCommand
    {
        public SetPasswordCommand(Guid? id, string newPassword, string confirmPassword)
        {
            Id = id;
            Password = newPassword;
            ConfirmPassword = confirmPassword;
        }

        public override bool IsValid()
        {
            ValidationResult = new SetPasswordCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}