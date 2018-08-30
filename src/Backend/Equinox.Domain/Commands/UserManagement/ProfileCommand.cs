using System;
using Equinox.Domain.Core.Commands;

namespace Equinox.Domain.Commands.UserManagement
{
    public abstract class ProfileCommand : Command
    {
        public Guid? Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Url { get; set; }
        public string Company { get; set; }
        public string Bio { get; set; }
        public string JobTitle { get; set; }
    }
}
