using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Jp.Application.ViewModels.ClientsViewModels
{
    public class RemoveClientSecretViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ClientId { get; set; }
    }
}
