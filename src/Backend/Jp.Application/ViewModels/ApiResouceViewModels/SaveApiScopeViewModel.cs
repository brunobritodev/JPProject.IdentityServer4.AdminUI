using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.ApiResouceViewModels
{
    public class SaveApiScopeViewModel
    {
        [Required]
        public string ResourceName { get; set; }
        /// <summary>
        /// Name of the scope. This is the value a client will use to request the scope.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Display name. This value will be used e.g. on the consent screen.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Description. This value will be used e.g. on the consent screen.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Specifies whether the user can de-select the scope on the consent screen. Defaults to false.
        /// </summary>
        public bool Required { get; set; } = false;

        /// <summary>
        /// Specifies whether the consent screen will emphasize this scope. Use this setting for sensitive or important scopes. Defaults to false.
        /// </summary>
        public bool Emphasize { get; set; } = false;

        /// <summary>
        /// Specifies whether this scope is shown in the discovery document. Defaults to true.
        /// </summary>
        public bool ShowInDiscoveryDocument { get; set; } = true;

        public IEnumerable<string> UserClaims { get; set; } = new HashSet<string>();
    }
}