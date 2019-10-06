using Jp.Domain.Validations.Client;

namespace Jp.Domain.Commands.Clients
{
    public class UpdateClientCommand : ClientCommand
    {
        public UpdateClientCommand(IdentityServer4.Models.Client client)
        {
            this.Client = client;
        }


        public override bool IsValid()
        {
            ValidationResult = new UpdateClientCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public UpdateClientCommand SetClientId(string id)
        {
            OldClientId = id;
            return this;
        }
    }
}