using IdentityServer4.EntityFramework.Entities;
using Jp.Domain.Commands.Clients;
using Jp.Domain.Validations.ApiResource;
using System.Collections.Generic;
using System.Linq;

namespace Jp.Domain.Commands.ApiResource
{
    public class SaveApiScopeCommand : ApiScopeCommand
    {

        public SaveApiScopeCommand(string resourceName, string name, string description, string displayName, bool emphasize, bool showInDiscoveryDocument, IEnumerable<string> userClaims)
        {
            ResourceName = resourceName;
            Name = name;
            Description = description;
            DisplayName = displayName;
            Emphasize = emphasize;
            ShowInDiscoveryDocument = showInDiscoveryDocument;
            UserClaims = userClaims;
        }

        public override bool IsValid()
        {
            ValidationResult = new SaveApiScopeCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public ApiScope ToEntity(IdentityServer4.EntityFramework.Entities.ApiResource savedApi)
        {
            return new ApiScope()
            {
                ApiResourceId = savedApi.Id,
                Description = Description,
                Required = Required,
                DisplayName = DisplayName,
                Emphasize = Emphasize,
                Name = Name,
                ShowInDiscoveryDocument = ShowInDiscoveryDocument,
                UserClaims = UserClaims.Select(s => new ApiScopeClaim() { Type = s }).ToList(),
            };
        }
    }
}