using Framework.Core.ServiceContracts;
using Framework.Presentation.AspNet.Extensions;
using Microsoft.AspNetCore.Http;

namespace Framework.Presentation.AspNet.Services;

public class AspNetCoreAuthenticatedUser(IHttpContextAccessor httpContextAccessor) : IAuthenticatedUser
{
    protected HttpContext? HttpContext => httpContextAccessor.HttpContext;

    public string? GetUserAgent() => HttpContext?.Request.Headers["User-Agent"];

    public string? GetUserIp() => HttpContext?.Connection.RemoteIpAddress?.ToString();

    public string? GetUsername() => HttpContext?.User.GetClaim("username");

    public bool IsCurrentUser(string userId) => string.Equals(GetSub(), userId, StringComparison.OrdinalIgnoreCase);

    public string? GetSub() => HttpContext?.User.GetClaim("sub");
}