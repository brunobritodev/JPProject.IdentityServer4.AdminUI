using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.ApiResouceViewModels
{
    public class RemoveApiResourceViewModel
    {
        public RemoveApiResourceViewModel(string name)
        {
            Name = name;
        }

        [Required]
        public string Name { get; set; }
    }
}