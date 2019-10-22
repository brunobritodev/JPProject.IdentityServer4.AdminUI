using IdentityServer4.Models;
using Newtonsoft.Json;

namespace Jp.Application.ViewModels.ClientsViewModels
{
    public class ClientViewModel : Client
    {
        [JsonIgnore]
        public string OldClientId { get; set; }
    }
}
