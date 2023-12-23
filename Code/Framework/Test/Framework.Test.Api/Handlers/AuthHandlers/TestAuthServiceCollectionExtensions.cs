using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Test.Api.Handlers.AuthHandlers;

public static class TestAuthServiceCollectionExtensions
{
    public static AuthenticationBuilder AddTestAuthentication(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(TestAuthConstants.Scheme)
            .RequireAuthenticatedUser()
            .Build();
        });

        return services.AddAuthentication(TestAuthConstants.Scheme)
            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthConstants.Scheme, options => { });
    }
}
