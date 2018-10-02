using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.UserViewModels
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
