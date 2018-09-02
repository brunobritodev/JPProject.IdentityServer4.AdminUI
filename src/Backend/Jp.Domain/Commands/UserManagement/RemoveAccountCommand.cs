using System;
using Jp.Domain.Validations.UserManagement;

namespace Jp.Domain.Commands.UserManagement
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