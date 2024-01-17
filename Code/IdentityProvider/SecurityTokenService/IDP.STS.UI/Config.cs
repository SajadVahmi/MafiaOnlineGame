using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IDP.STS.UI;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
       new ApiScope[]
       {
            new ApiScope("players"),
       };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            //players swagger api ui client
            new Client
            {

                ClientId = "players-api-swagger",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RedirectUris = { "http://localhost:5171/swagger/oauth2-redirect.html" },
                AllowedCorsOrigins = { "http://localhost:5171" },
                AllowOfflineAccess = true,
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "players"   }

            }
        };


}
