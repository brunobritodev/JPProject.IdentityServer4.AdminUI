using System.ComponentModel.DataAnnotations;

namespace Equinox.Application.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        public string UsernameOrEmail { get; set; }
    }
}
