using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.ClientsViewModels
{
    public class RemoveClientClaimViewModel
    {
        public RemoveClientClaimViewModel(string clientId, in int id)
        {
            ClientId = clientId;
            Id = id;
        }

        [Required]
        public int Id { get; set; }
        [Required]
        public string ClientId { get; set; }
    }
}