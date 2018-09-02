using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels
{
    public class SocialViewModel
    {
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Picture")]
        public string Picture { get; set; }

        public string ProviderId { get; set; }

        public string Provider { get; set; }
    }
}