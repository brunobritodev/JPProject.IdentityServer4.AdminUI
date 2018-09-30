using Jp.Domain.Validations.Client;

namespace Jp.Domain.Commands.Client
{
    public class RemoveClientSecretCommand : ClientSecretCommand
    {

        public RemoveClientSecretCommand(int id, string clientId)
        {
            Id = id;
            ClientId = clientId;
        }


        public override bool IsValid()
        {
            ValidationResult = new RemoveClientSecretCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}