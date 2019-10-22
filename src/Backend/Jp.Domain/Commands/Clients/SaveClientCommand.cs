using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Jp.Domain.Validations.Client;
using System;
using System.Collections.Generic;
using Jp.Domain.Core.StringUtils;

namespace Jp.Domain.Commands.Clients
{
    public class SaveClientCommand : ClientCommand
    {
        public ClientType ClientType { get; }

        public SaveClientCommand(string clientId, string name, string clientUri, string logoUri, string description,
            ClientType clientType, string postLogoutUri)
        {
            this.Client = new Client()
            {
                ClientId = clientId,
                ClientName = name,
                ClientUri = clientUri,
                LogoUri = logoUri,
                Description = description,
            };

            if (postLogoutUri.IsMissing())
                Client.PostLogoutRedirectUris = new List<string>() { postLogoutUri };
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
                    ConfigureWebHybrid();
                    break;
                case ClientType.Native:
                    ConfigureNative();

                    break;
                case ClientType.Machine:
                    ConfigureMachine();

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ClientType));
            }
        }

        public override bool IsValid()
        {
            ValidationResult = new SaveClientCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private void ConfigureDevice()
        {
            Client.AllowedGrantTypes.Add(GrantType.DeviceFlow);
            Client.RequireClientSecret = false;
            Client.AllowOfflineAccess = true;
        }

        private void ConfigureWebImplicit()
        {
            Client.AllowedGrantTypes.Add(GrantType.Implicit);
            Client.AllowAccessTokensViaBrowser = true;
        }

        private void ConfigureWebHybrid()
        {
            Client.AllowedGrantTypes.Add(GrantType.Hybrid);
        }

        private void ConfigureNative()
        {
            Client.AllowedGrantTypes.Add(GrantType.Hybrid);
        }

        private void ConfigureMachine()
        {
            Client.AllowedGrantTypes.Add(GrantType.ResourceOwnerPassword);
            Client.AllowedGrantTypes.Add(GrantType.ClientCredentials);
        }
    }
}