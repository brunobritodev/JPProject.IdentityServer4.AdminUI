using Jp.Domain.Validations.Client;

namespace Jp.Domain.Commands.Client
{
    public class RemoveClientClaimCommand : ClientClaimCommand
    {

        public RemoveClientClaimCommand(int id, string clientId)
        {
            Id = id;
            ClientId = clientId;
        }
        public override bool IsValid()
        {
            ValidationResult = new RemoveClientClaimCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}