using Jp.Domain.Core.Bus;
using Jp.Domain.Interfaces;
using Jp.Domain.Models;
using Jp.Infra.CrossCutting.Identity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jp.Infra.CrossCutting.Identity.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<UserIdentityRole> _roleManager;
        private ILogger<UserService> _logger;
        private IMediatorHandler _bus;

        public RoleService(
            RoleManager<UserIdentityRole> roleManager,
            IEmailSender emailSender,
            IMediatorHandler bus,
            ILoggerFactory loggerFactory)
        {
            _roleManager = roleManager;
            _bus = bus;
            _logger = loggerFactory.CreateLogger<UserService>(); ;

        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles.Select(s => new Role() { Id = s.Id, Name = s.Name }).ToList();
        }
    }
}
