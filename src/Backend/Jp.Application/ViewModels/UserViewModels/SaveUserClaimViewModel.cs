using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Jp.Application.ViewModels.UserViewModels
{
    public class SaveUserClaimViewModel
    {
        [Required]
        public string Value { get; set; }
        [Required]
        public string Type { get; set; }
        [JsonIgnore]
        public string Username { get; set; }
    }
}
