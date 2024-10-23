using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityProvider.ServiceHost;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope("players"),
        };

    public static IEnumerable<Client> Clients =>
        new[]
        {
            //players swagger api ui client
            new Client
            {

                ClientId = "players-api-swagger",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = false,
                RedirectUris = { "https://localhost:7002/swagger/oauth2-redirect.html" },
                AllowedCorsOrigins = { "https://localhost:7002" },
                AllowOfflineAccess = true,
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "players"   }

            }
        };


}
