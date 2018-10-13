using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.User
{
    public abstract class UserLoginCommand : Command
    {
        public string Username { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
}