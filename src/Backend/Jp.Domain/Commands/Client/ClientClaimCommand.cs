using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.Client
{
    public abstract class ClientClaimCommand : Command
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string Type { get; set; }

        public string Value { get; set; }
    }
}