namespace Jp.Application.ViewModels.RoleViewModels
{
    public class RemoveUserFromRoleViewModel
    {
        public RemoveUserFromRoleViewModel(string role, string username)
        {
            Role = role;
            Username = username;
        }

        public string Role { get; set; }
        public string Username { get; set; }
    }
}