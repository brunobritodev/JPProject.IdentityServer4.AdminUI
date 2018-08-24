using Equinox.Infra.CrossCutting.Identity.Entities.Identity;

namespace Equinox.WebApi.ViewModels
{
    public class UserProfile
    {
        public UserProfile() { }
        public UserProfile(UserIdentity userIdentity)
        {
            Email = userIdentity.Email;
            PhoneNumber = userIdentity.PhoneNumber;
            UserName = userIdentity.UserName;
            Picture = userIdentity.Picture;
            Name = userIdentity.Name;
            Url = userIdentity.Url;
            Company = userIdentity.Company;
            JobTitle = userIdentity.JobTitle;
            Bio = userIdentity.Bio;
        }

        public string Bio { get; set; }

        public string JobTitle { get; set; }

        public string Company { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
