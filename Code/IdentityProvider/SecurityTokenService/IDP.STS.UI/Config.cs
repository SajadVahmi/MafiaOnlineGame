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
                ClientId = "playersswaggerapiui",

                ClientName = "Players Swagger API UI",

                AllowedGrantTypes = GrantTypes.Implicit,

                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                RedirectUris={"http://localhost:5171/swagger/oauth2-redirect.html"},

                PostLogoutRedirectUris={ "http://localhost:5171/swagger" },

                AllowedScopes = {
                    "players",

                    IdentityServerConstants.StandardScopes.OpenId,

                    IdentityServerConstants.StandardScopes.Profile
                },
                AllowAccessTokensViaBrowser = true,
            }
        };


}
