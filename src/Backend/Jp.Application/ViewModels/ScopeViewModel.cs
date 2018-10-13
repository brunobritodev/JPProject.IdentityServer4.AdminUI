using System.Collections.Generic;

namespace Jp.Application.ViewModels
{
    public class ScopeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public IEnumerable<ClaimViewModel> UserClaims { get; set; }

    }
}