using Jp.Domain.Validations.Client;

namespace Jp.Domain.Commands.Client
{
    public class SaveClientCommand : ClientCommand
    {
        public string Description { get; }
        public ClientType ClientType { get; }

        public SaveClientCommand(string clientId, string name, string clientUri, string logoUri, string description, ClientType clientType)
        {
            this.Client = new IdentityServer4.Models.Client()
            {
                ClientId = clientId,
                ClientName = name,
                ClientUri = clientUri,
                LogoUri = logoUri,
            };
            Description = description;
            ClientType = clientType;
        }
        public override bool IsValid()
        {
            ValidationResult = new SaveClientCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}