using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.User
{
    public abstract class UserLoginCommand : Command
    {
        public string Username { get; protected set; }
        public string LoginProvider { get; protected set; }
        public string ProviderKey { get; protected set; }
    }
}