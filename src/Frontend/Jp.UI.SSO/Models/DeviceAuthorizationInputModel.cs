using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jp.UI.SSO.Controllers.Consent;

namespace Jp.UI.SSO.Models
{
    public class DeviceAuthorizationInputModel : ConsentInputModel
    {
        public string UserCode { get; set; }
    }
}
