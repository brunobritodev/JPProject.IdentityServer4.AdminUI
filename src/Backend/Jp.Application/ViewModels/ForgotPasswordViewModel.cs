using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        public string UsernameOrEmail { get; set; }
    }
}
