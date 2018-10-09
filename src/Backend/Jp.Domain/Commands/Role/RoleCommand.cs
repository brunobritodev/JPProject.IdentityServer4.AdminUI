using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.Role
{
    public abstract class RoleCommand : Command
    {
        public string Name { get; set; }
    }
}