using System.ComponentModel.DataAnnotations;

namespace Equinox.Application.ViewModels
{
    public class ConfirmEmailViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
