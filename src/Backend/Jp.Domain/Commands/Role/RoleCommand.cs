using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.Role
{
    public abstract class RoleCommand : Command
    {
        public string OldName { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
    }
}