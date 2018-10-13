using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.ClientsViewModels
{
    public class RemoveClientClaimViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ClientId { get; set; }
    }
}