using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.ClientsViewModels
{
    public class RemoveClientViewModel
    {
        [Required]
        public string ClientId { get; set; }

    }
}