using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.ClientsViewModels
{
    public class RemoveClientViewModel
    {
        public RemoveClientViewModel(string clientId)
        {
            ClientId = clientId;
        }

        [Required]
        public string ClientId { get; set; }

    }
}