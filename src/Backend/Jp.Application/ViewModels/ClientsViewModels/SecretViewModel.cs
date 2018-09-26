using System;
using System.Collections.Generic;
using System.Text;

namespace Jp.Application.ViewModels.ClientsViewModels
{
    public class SecretViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }

        public HashType Hash { get; set; } = 0;
        public string Type { get; set; }
    }
    public enum HashType
    {
        Sha256,
        Sha512
    }
}
