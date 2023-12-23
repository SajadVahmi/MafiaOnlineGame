using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Logging;

namespace Framework.Test.Api.Handlers.AuthHandlers;

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly MockAuthUser _mockAuthUser;

    public TestAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        MockAuthUser mockAuthUser)
        : base(options, logger, encoder, clock)
    {
        _mockAuthUser = mockAuthUser;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (_mockAuthUser.Claims.Count == 0)
            return Task.FromResult(AuthenticateResult.Fail("Mock auth user not configured."));

        var identity = new ClaimsIdentity(_mockAuthUser.Claims, TestAuthConstants.Scheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, TestAuthConstants.Scheme);

        var result = AuthenticateResult.Success(ticket);
        return Task.FromResult(result);
    }
}
