using System;
using Equinox.Domain.Core.Events;

namespace Equinox.Domain.Events.User
{
    public class UserRegisteredeEvent : Event
    {
        public Guid Id { get; }
        public string Username { get; }
        public string UserEmail { get; }

        public UserRegisteredeEvent(Guid id, string userName, string userEmail)
        {
            Id = id;
            Username = userName;
            UserEmail = userEmail;
        }
    }
}