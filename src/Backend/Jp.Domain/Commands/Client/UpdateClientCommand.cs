using Jp.Domain.Validations.Client;

namespace Jp.Domain.Commands.Client
{
    public class UpdateClientCommand : ClientCommand
    {
        

        public UpdateClientCommand(IdentityServer4.Models.Client client, string oldClientId)
        {
            OldClientId = oldClientId;
            this.Client = client;
        }


        public override bool IsValid()
        {
            ValidationResult = new UpdateClientCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}