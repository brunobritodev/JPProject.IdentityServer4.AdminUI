using Jp.Domain.Validations.Client;

namespace Jp.Domain.Commands.Client
{
    public class RemoveClientCommand : ClientCommand
    {

        public RemoveClientCommand(string clientId)
        {
            this.Client = new IdentityServer4.Models.Client() { ClientId = clientId };
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveClientCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}