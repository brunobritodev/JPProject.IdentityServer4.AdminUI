using Jp.UI.SSO.Controllers.Consent;

namespace Jp.UI.SSO.Models
{
    public class DeviceAuthorizationViewModel : ConsentViewModel
    {
        public string UserCode { get; set; }
        public bool ConfirmUserCode { get; set; }
    }
}