using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.RoleViewModels
{
    public class RemoveRoleViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
