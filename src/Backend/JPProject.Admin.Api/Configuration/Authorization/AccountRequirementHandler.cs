using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace JPProject.Admin.Api.Configuration.Authorization
{
    public class AccountRequirementHandler : AuthorizationHandler<AccountRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountRequirementHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AccountRequirement requirement)
        {

            var httpMethod = _httpContextAccessor.HttpContext.Request.Method;

            if (HttpMethods.IsGet(httpMethod) || HttpMethods.IsHead(httpMethod))
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }
            else
            {
                if (context.User.HasClaim("is4-rights", "manager") ||
                    context.User.IsInRole("Administrator"))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }
}