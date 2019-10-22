using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.RoleViewModels
{
    public class RemoveRoleViewModel
    {
        public RemoveRoleViewModel(string name)
        {
            Name = name;
        }

        [Required]
        public string Name { get; set; }
    }
}
