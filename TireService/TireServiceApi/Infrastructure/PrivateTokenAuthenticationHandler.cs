using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using MeisterSoft.Bim.Core.Domain.Services;
using MeisterSoft.Bim.Shared.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace webapi.Infrastructure
{
    public class PrivateTokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SecretKeyService _secretKeyService;

        public PrivateTokenAuthenticationHandler(SecretKeyService secretKeyService,
            IHttpContextAccessor httpContextAccessor,
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _secretKeyService = secretKeyService;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var privateToken = _httpContextAccessor.HttpContext?.Request?.Headers["Authorization"].ToString()
                .Replace("PrivateToken ", "", StringComparison.InvariantCulture);

            var userContext = await _secretKeyService.ValidateSecretKey(
                new SecretKeyModel { PrivateToken = privateToken });

            var userRoleClaimValue = string.Join(',', userContext.UserRoles);
            if (userRoleClaimValue.Contains(',', StringComparison.InvariantCulture))
                userRoleClaimValue = $"[{userRoleClaimValue}]";

            var claims = new[]
            {
                new Claim("username", userContext.UserName),
                new Claim("sid", userContext.Id.ToString()),
                new Claim("display_name", userContext.DisplayName),
                new Claim("user_role", userRoleClaimValue)
            };
            var identity = new ClaimsIdentity(claims, "Passport", "username", "user_role");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            return AuthenticateResult.Success(
                new AuthenticationTicket(claimsPrincipal, "PrivateToken"));
        }
    }
}
