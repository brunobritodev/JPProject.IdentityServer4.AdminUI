using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.UserViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        public string UsernameOrEmail { get; set; }
    }
}
