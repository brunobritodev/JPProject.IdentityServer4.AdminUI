using System;
using IdentityModel;
using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.Validations.Client;

namespace Jp.Domain.Commands.Clients
{
    public class SaveClientSecretCommand : ClientSecretCommand
    {

        public SaveClientSecretCommand(string clientId, string description, string value, string type, DateTime? expiration,
            int hashType)
        {
            ClientId = clientId;
            Description = description;
            Value = value;
            Type = type;
            Expiration = expiration;
            Hash = hashType;
        }
        public override bool IsValid()
        {
            ValidationResult = new SaveClientSecretCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public string GetValue()
        {
            switch (Hash)
            {
                case 0:
                    return Value.ToSha256();
                case 1:
                    return Value.ToSha512();
                default:
                    throw new ArgumentException(nameof(Hash));
            }
        }

        public ClientSecret ToEntity(Client savedClient)
        {
            return new ClientSecret
            {
                Client = savedClient,
                Description = Description,
                Expiration = Expiration,
                Type = Type,
                Value = GetValue()
            };
        }
    }
}