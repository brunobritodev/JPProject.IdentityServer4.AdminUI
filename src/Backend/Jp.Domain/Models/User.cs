using Jp.Domain.Commands.User;
using Jp.Domain.Interfaces;
using System;

namespace Jp.Domain.Models
{
    public class User : IDomainUser
    {
        // EF Constructor
        public User() { }
        public User(Guid id, string email, string name, string userName, string phoneNumber, string picture)
        {
            Id = id;
            Email = email;
            Name = name;
            UserName = userName;
            PhoneNumber = phoneNumber;
            Picture = picture;
        }

        public User(Guid id, string email, bool emailConfirmed, string passwordHash, string securityStamp, string phoneNumber, bool phoneNumberConfirmed, bool twoFactorEnabled, DateTimeOffset? lockoutEnd, bool lockoutEnabled, int accessFailedCount, string userName, string picture, string url, string name, string company, string bio, string jobTitle)
        {
            Id = id;
            Email = email;
            EmailConfirmed = emailConfirmed;
            PasswordHash = passwordHash;
            SecurityStamp = securityStamp;
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            TwoFactorEnabled = twoFactorEnabled;
            LockoutEnd = lockoutEnd;
            LockoutEnabled = lockoutEnabled;
            AccessFailedCount = accessFailedCount;
            UserName = userName;
            Picture = picture;
            Url = url;
            Name = name;
            Company = company;
            Bio = bio;
            JobTitle = jobTitle;
        }

        public User(Guid id, string email, bool emailConfirmed, string name, string securityStamp, int accessFailedCount, string bio, string company, string jobTitle, bool lockoutEnabled, DateTimeOffset? lockoutEnd, string phoneNumber, bool phoneNumberConfirmed, string picture, bool twoFactorEnabled, string url, string userName)
        {
            Id = id;
            Email = email;
            EmailConfirmed = emailConfirmed;
            Name = name;
            SecurityStamp = securityStamp;
            AccessFailedCount = accessFailedCount;
            Bio = bio;
            Company = company;
            JobTitle = jobTitle;
            LockoutEnabled = lockoutEnabled;
            LockoutEnd = lockoutEnd;
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            Picture = picture;
            TwoFactorEnabled = twoFactorEnabled;
            Url = url;
            UserName = userName;
        }

        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public bool EmailConfirmed { get; private set; }
        public string PasswordHash { get; private set; }
        public string SecurityStamp { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool PhoneNumberConfirmed { get; private set; }
        public bool TwoFactorEnabled { get; private set; }
        public DateTimeOffset? LockoutEnd { get; private set; }
        public bool LockoutEnabled { get; private set; }
        public int AccessFailedCount { get; private set; }
        public string UserName { get; private set; }

        public string Picture { get; private set; }
        public string Url { get; private set; }
        public string Name { get; private set; }
        public string Company { get; private set; }
        public string Bio { get; private set; }
        public string JobTitle { get; private set; }


        public void UpdateInfo(UpdateUserCommand request)
        {
            Email = request.Email;
            EmailConfirmed = request.EmailConfirmed;
            AccessFailedCount = request.AccessFailedCount;
            LockoutEnabled = request.LockoutEnabled;
            LockoutEnd = request.LockoutEnd;
            Name = request.Name;
            TwoFactorEnabled = request.TwoFactorEnabled;
            PhoneNumber = request.PhoneNumber;
            PhoneNumberConfirmed = request.PhoneNumberConfirmed;
        }
    }
}
