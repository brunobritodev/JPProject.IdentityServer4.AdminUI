using IdentityServer4.Models;

namespace Jp.Application.ViewModels.ApiResouceViewModels
{
    public class UpdateApiResourceViewModel : ApiResource
    {
        public string OldApiResourceId { get; set; }
    }
}
