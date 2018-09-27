using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.ClientsViewModels
{
    public class SaveClientPropertyViewModel
    {
        [Required]
        public string Value { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public string ClientId { get; set; }
    }
}