using System;
using Jp.Domain.Core.Events;

namespace Jp.Domain.Events.User
{
    public class AdminChangedPasswordEvent : Event
    {
        public string Username { get; }

        public AdminChangedPasswordEvent(Guid userId, string username)
        {
            Username = username;
            AggregateId = userId.ToString();
        }
    }
}