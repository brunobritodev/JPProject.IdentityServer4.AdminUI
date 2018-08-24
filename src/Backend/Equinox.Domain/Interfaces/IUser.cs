using System;

namespace Equinox.Domain.Interfaces
{
    /// <summary>
    /// Interface to propagate user id type
    /// </summary>
    public interface IDomainUser : IUser<Guid>
    {

    }
    public interface IUser<TUserId>
    {
        TUserId Id { get; set; }
        string Email { get; set; }
        bool EmailConfirmed { get; set; }
        string PasswordHash { get; set; }
        string SecurityStamp { get; set; }
        string PhoneNumber { get; set; }
        bool PhoneNumberConfirmed { get; set; }
        bool TwoFactorEnabled { get; set; }
        DateTimeOffset? LockoutEnd { get; set; }
        bool LockoutEnabled { get; set; }
        int AccessFailedCount { get; set; }
        string UserName { get; set; }
        string Picture { get; set; }
        string Url { get; set; }
        string Name { get; set; }
        string Company { get; set; }
        string Bio { get; set; }
        string JobTitle { get; set; }
    }
}