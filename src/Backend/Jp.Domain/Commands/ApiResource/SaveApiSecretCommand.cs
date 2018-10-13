using System;
using IdentityModel;
using Jp.Domain.Commands.Client;
using Jp.Domain.Validations.ApiResource;

namespace Jp.Domain.Commands.ApiResource
{
    public class SaveApiSecretCommand : ApiSecretCommand
    {

        public SaveApiSecretCommand(string resourceName, string description, string value, string type, DateTime? expiration,
            int hashType)
        {
            ResourceName = resourceName;
            Description = description;
            Value = value;
            Type = type;
            Expiration = expiration;
            Hash = hashType;
        }
        public override bool IsValid()
        {
            ValidationResult = new SaveApiSecretCommandValidation().Validate(this);
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
    }
}