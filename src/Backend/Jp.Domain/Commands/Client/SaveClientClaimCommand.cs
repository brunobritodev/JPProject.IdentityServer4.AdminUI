using Jp.Domain.Validations.Client;

namespace Jp.Domain.Commands.Client
{
    public class SaveClientClaimCommand : ClientClaimCommand
    {

        public SaveClientClaimCommand(string clientId, string type, string value)
        {
            Type = type;
            ClientId = clientId;
            Value = value;
        }
        public override bool IsValid()
        {
            ValidationResult = new SaveClientClaimCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}