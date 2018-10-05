using System;
using System.Collections.Generic;
using System.Text;

namespace Jp.Domain.Models
{
   public class UserLogin
    {
        public string LoginProvider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderKey { get; set; }
    }
}
