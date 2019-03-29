using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.Role
{
    public abstract class RoleCommand : Command
    {
        public string OldName { get; protected set; }
        public string Name { get; protected set; }
        public string Username { get; protected set; }
    }
}