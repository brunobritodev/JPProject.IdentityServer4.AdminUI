using System.Collections.Generic;
using Jp.Domain.Commands.Client;
using Jp.Domain.Validations.ApiResource;

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

    }
}