using System;
using System.Collections.Generic;
using System.Text;
using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.Client
{
    public abstract class ClientSecretCommand : Command
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string Description { get; set; }

        public string Value { get; set; }
        public DateTime? Expiration { get; set; }

        public int Hash { get; set; } = 0;
        public string Type { get; set; }
    }
}
