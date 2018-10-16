using System.ComponentModel.DataAnnotations;
using IdentityServer4.Models;

namespace Jp.Application.ViewModels.ClientsViewModels
{
    public class ClientViewModel : Client
    {
        [Required]
        public string OldClientId { get; set; }
    }
}
