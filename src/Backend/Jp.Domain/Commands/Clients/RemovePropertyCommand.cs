using Jp.Domain.Validations.Client;

namespace Jp.Domain.Commands.Clients
{
    public class RemovePropertyCommand : ClientPropertyCommand
    {

        public RemovePropertyCommand(int id, string clientId)
        {
            Id = id;
            ClientId = clientId;
        }


        public override bool IsValid()
        {
            ValidationResult = new RemovePropertyCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}