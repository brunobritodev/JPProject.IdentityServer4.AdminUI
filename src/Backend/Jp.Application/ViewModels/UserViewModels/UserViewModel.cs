using System;
using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.UserViewModels
{
    public class UserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone] [Display(Name = "Telephone")] public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Name { get; set; }


        [Display(Name = "Picture")] public string Picture { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        public string Url { get; set; }
        public string Company { get; set; }
        public string Bio { get; set; }
        public string JobTitle { get; set; }
        public Guid Id { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string SecurityStamp { get; set; }
    }
}