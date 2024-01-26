using System.Security.Claims;

namespace Framework.Test.Api.Handlers.AuthHandlers;

public class MockAuthUser(params Claim[] claims)
{
    public List<Claim> Claims { get; private set; } = claims.ToList();
}
