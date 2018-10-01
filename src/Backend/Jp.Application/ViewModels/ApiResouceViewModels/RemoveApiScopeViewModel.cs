using System.ComponentModel.DataAnnotations;

namespace Jp.Application.ViewModels.ApiResouceViewModels
{
    public class RemoveApiScopeViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ResourceName { get; set; }
    }
}