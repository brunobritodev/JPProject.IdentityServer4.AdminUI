using System;
using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.Client
{
    public abstract class ClientPropertyCommand : Command
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string Key { get; set; }

        public string Value { get; set; }
    }
}