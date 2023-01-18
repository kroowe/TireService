using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace TireServiceApi.Extensions
{
    /// <summary />
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            var signingKey = GetSignInKey(services);
            var tokenValidationParameters = GetTokenValidationParameters(configuration, signingKey);

            services
                .AddAuthentication(options =>
                {
                    options.DefaultChallengeScheme = "smart";
                    options.DefaultSignInScheme = "smart";
                    options.DefaultAuthenticateScheme = "smart";
                })
                .AddPolicyScheme("smart", "Bearer or PrivateToken", options =>
                {
                    options.ForwardDefaultSelector = context =>
                    {
                        var authHeader = context.Request.Headers["Authorization"];
                        if (authHeader.Count > 0
                            && authHeader[0].StartsWith("PrivateToken", StringComparison.InvariantCulture))
                            return "PrivateToken";
                        return JwtBearerDefaults.AuthenticationScheme;
                    };
                })
                .AddScheme<AuthenticationSchemeOptions, PrivateTokenAuthenticationHandler>(
                    "PrivateToken", "PrivateToken", x => { })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = tokenValidationParameters;
                });

            return services;
        }

        private static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration,
            SecurityKey signingKey)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidIssuer = configuration["Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["Audience"],
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ClockSkew = TimeSpan.Zero,
                RoleClaimType = "user_role",
                NameClaimType = "username"
            };
        }

        private static SymmetricSecurityKey GetSignInKey(IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var authClient = serviceProvider.GetRequiredService<IAuthenticationClient>();
                var initializerUserAccountSetting =
                    serviceProvider.GetRequiredService<IOptions<InitializerUserAccountSettings>>();
                var token = authClient.Login(new AuthenticationClientInfo
                {
                    Username = initializerUserAccountSetting.Value.InitializerUserName,
                    Password = initializerUserAccountSetting.Value.InitializerUserPhrase
                }).Result.AccessToken;
                authClient.SetJwtToken(token);

                return new SymmetricSecurityKey(authClient.GetSignInKey().GetAwaiter().GetResult().Key);
            }
        }
    }
}
