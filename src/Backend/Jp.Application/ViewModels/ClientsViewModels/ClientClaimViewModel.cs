using System;
using System.Collections.Generic;
using System.Text;
using IdentityServer4.EntityFramework.Entities;

namespace Jp.Application.ViewModels.ClientsViewModels
{
    public class ClientClaimViewModel
    {
        public ClientClaimViewModel() { }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
