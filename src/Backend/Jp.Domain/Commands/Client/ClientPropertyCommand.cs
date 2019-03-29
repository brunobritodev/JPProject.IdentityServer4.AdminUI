using System;
using Jp.Domain.Core.Commands;

namespace Jp.Domain.Commands.Client
{
    public abstract class ClientPropertyCommand : Command
    {
        public int Id { get; protected set; }
        public string ClientId { get; protected set; }
        public string Key { get; protected set; }

        public string Value { get; protected set; }
    }
}