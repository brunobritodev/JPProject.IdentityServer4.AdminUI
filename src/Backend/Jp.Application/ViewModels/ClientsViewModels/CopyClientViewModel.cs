using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.ClientsViewModels
{
    public class CopyClientViewModel
    {
        [Required]
        public string ClientId { get; set; }

    }
}