using System.Security.Claims;

namespace Framework.Test.Api.Handlers.AuthHandlers;

public class MockAuthUser
{
    public List<Claim> Claims { get; private set; } = new();

    public MockAuthUser(params Claim[] claims)
        => Claims = claims.ToList();
}
