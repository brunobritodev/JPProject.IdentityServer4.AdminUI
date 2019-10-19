using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.UserViewModels
{
    public class SaveUserRoleViewModel
    {
        [Required]
        public string Role { get; set; }
        [JsonIgnore]
        public string Username { get; set; }
    }
}