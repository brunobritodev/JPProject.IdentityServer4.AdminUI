using IdentityServer4.Models;

namespace Jp.Application.ViewModels.ClientsViewModels
{
    public class ClientViewModel : Client
    {
        public string OldClientId { get; set; }
    }
}
