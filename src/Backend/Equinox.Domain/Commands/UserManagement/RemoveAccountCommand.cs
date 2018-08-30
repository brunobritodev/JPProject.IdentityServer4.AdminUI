using System;
using Equinox.Domain.Validations.UserManagement;

namespace Equinox.Domain.Commands.UserManagement
{
    public class RemoveAccountCommand:ProfileCommand
    {
        public RemoveAccountCommand(Guid? id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveAccountCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}