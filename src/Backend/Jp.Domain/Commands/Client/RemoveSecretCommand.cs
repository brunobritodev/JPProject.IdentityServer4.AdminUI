using Jp.Domain.Validations.Client;

namespace Jp.Domain.Commands.Client
{
    public class RemoveSecretCommand : ClientSecretCommand
    {

        public RemoveSecretCommand(int id, string clientId)
        {
            Id = id;
            ClientId = clientId;
        }


        public override bool IsValid()
        {
            ValidationResult = new RemoveSecretCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}