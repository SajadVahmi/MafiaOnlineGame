using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Framework.Test.Api.Handlers.AuthHandlers;

public class TestAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    MockAuthUser mockAuthUser)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (mockAuthUser.Claims.Count == 0)
            return Task.FromResult(AuthenticateResult.Fail("Mock auth user not configured."));

        var identity = new ClaimsIdentity(mockAuthUser.Claims, TestAuthConstants.Scheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, TestAuthConstants.Scheme);

        var result = AuthenticateResult.Success(ticket);
        return Task.FromResult(result);
    }
}
