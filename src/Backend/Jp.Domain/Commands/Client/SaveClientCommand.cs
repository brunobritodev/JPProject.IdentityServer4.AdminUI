using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Jp.Domain.Validations.Client;
using System;
using MediatR;

namespace Jp.Domain.Commands.Client
{
    public class SaveClientCommand : ClientCommand
    {
        public ClientType ClientType { get; }

        public SaveClientCommand(string clientId, string name, string clientUri, string logoUri, string description, ClientType clientType)
        {
            this.Client = new IdentityServer4.Models.Client()
            {
                ClientId = clientId,
                ClientName = name,
                ClientUri = clientUri,
                LogoUri = logoUri,
                Description = description,
            };
            ClientType = clientType;
        }

        public IdentityServer4.EntityFramework.Entities.Client ToEntity()
        {
            PrepareClientTypeForNewClient();
            return Client.ToEntity();
        }

        private void PrepareClientTypeForNewClient()
        {
            switch (ClientType)
            {
                case ClientType.Empty:
                    break;
                case ClientType.Device:
                    ConfigureDevice();
                    break;
                case ClientType.WebImplicit:
                    ConfigureWebImplicit();

                    break;
                case ClientType.WebHybrid:
                    ConfigureWebHybrid();
                    break;
                case ClientType.Spa:
                    ConfigureSpa();
                    break;
                case ClientType.Native:
                    ConfigureNative();

                    break;
                case ClientType.Machine:
                    ConfigureMachine();

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override bool IsValid()
        {
            ValidationResult = new SaveClientCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private void ConfigureDevice()
        {
            Client.AllowedGrantTypes = GrantTypes.DeviceFlow;
            Client.RequireClientSecret = false;
            Client.AllowOfflineAccess = true;
        }

        private void ConfigureWebImplicit()
        {
            Client.AllowedGrantTypes = GrantTypes.Implicit;
            Client.AllowAccessTokensViaBrowser = true;
        }

        private void ConfigureWebHybrid()
        {
            Client.AllowedGrantTypes = GrantTypes.Hybrid;
        }

        private void ConfigureSpa()
        {
            Client.AllowedGrantTypes = GrantTypes.Implicit;
            Client.AllowAccessTokensViaBrowser = true;
        }

        private void ConfigureNative()
        {
            Client.AllowedGrantTypes = GrantTypes.Hybrid;
        }

        private void ConfigureMachine()
        {
            Client.AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials;
        }
    }
}