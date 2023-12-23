using Framework.Test.Api.Handlers.AuthHandlers;
using System.Security.Claims;

namespace Framework.Test.Api.Builders;

public class MockAuthUserBuilder
{
    private List<Claim> _claims = new();

    public static MockAuthUserBuilder Instantiate() => new();

    protected MockAuthUserBuilder() { }

    public MockAuthUserBuilder WithClaim(string claimName, string claimValue)
    {
        _claims.Add(new Claim(claimName, claimValue));

        return this;
    }

    public MockAuthUser Build()=> new MockAuthUser(_claims.ToArray());
        
}
