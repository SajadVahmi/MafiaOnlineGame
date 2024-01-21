using Framework.Core.Contracts;
using Framework.Presentation.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;

namespace Framework.Presentation.AspNetCore.Services;

public class AspNetCoreAuthenticatedUser : IAuthenticatedUser
{
    private IHttpContextAccessor _httpContextAccessor;

    private HttpContext _httpContext => _httpContextAccessor.HttpContext;

    public AspNetCoreAuthenticatedUser(IHttpContextAccessor httpContextAccessor)
    {

        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetUserAgent() => _httpContext?.Request.Headers["User-Agent"];

    public string? GetUserIp() => _httpContext?.Connection?.RemoteIpAddress?.ToString();

    public string? GetUsername() => _httpContext?.User?.GetClaim("username");

    public bool IsCurrentUser(string userId) => string.Equals(GetSub()?.ToString(), userId, StringComparison.OrdinalIgnoreCase);

    public string? GetSub() => _httpContext?.User?.GetClaim("sub");
}
