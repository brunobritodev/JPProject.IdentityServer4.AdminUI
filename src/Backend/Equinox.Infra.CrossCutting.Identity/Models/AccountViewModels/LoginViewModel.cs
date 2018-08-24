using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Equinox.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class LoginViewModel : LoginInputModel
    {
        public bool AllowRememberLogin { get; set; }
        public bool EnableLocalLogin { get; set; }

        public IEnumerable<ExternalProviderViewModel> ExternalProviders { get; set; }
        public IEnumerable<ExternalProviderViewModel> VisibleExternalProviders => ExternalProviders.Where(x => !String.IsNullOrWhiteSpace(x.DisplayName));

        public bool IsExternalLoginOnly => EnableLocalLogin == false && ExternalProviders?.Count() == 1;
        public string ExternalLoginScheme => IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;

        public bool IsUsernameEmail()
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(Username, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}
