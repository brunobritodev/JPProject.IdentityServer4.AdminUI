using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.ApiResouceViewModels
{
    public class RemoveApiResourceViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}