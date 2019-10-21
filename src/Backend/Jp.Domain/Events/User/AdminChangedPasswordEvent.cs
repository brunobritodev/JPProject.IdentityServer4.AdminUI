using Jp.Domain.Core.Events;
using System;

namespace Jp.Domain.Events.User
{
    public class AdminChangedPasswordEvent : Event
    {
        public string Username { get; }

        public AdminChangedPasswordEvent(Guid userId, string username)
            : base(EventTypes.Success)
        {
            Username = username;
            AggregateId = userId.ToString();
        }
    }
}