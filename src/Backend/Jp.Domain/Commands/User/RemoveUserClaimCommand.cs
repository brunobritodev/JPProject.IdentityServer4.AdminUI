using System;
using Jp.Domain.Validations.User;

namespace Jp.Domain.Commands.User
{
    public class RemoveUserClaimCommand : UserClaimCommand
    {
        public RemoveUserClaimCommand(string username, string type)
        {
            Type = type;
            Username = username;
        }
        public override bool IsValid()
        {
            ValidationResult = new RemoveUserClaimCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}