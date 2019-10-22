using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.UserViewModels
{
    public class ConfirmEmailViewModel
    {
        [JsonIgnore]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Code { get; set; }

    }
}
