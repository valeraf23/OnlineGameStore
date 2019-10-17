using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace OnlineGameStore.IDP
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles", "Your role(s)", new[] {"role"})
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("onlinegamestoreapi", "online game store api", new[] {"role", "name"})
            };
        }

        public static IEnumerable<Client> GetClients(string clientHost)
        {
            return new[]
            {
                new Client
                {
                    RequireConsent = false,
                    ClientId = "online_game_store_client",
                    ClientName = "online game store",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "onlinegamestoreapi"
                    },
                    RedirectUris =
                    {
                        $"{clientHost}/assets/signin-oidc.html",
                        $"{clientHost}/assets/silent-redirect.html"
                    },
                    PostLogoutRedirectUris = {$"{clientHost}/?postLogout=true"},
                    IdentityTokenLifetime = 120,
                    AccessTokenLifetime = 120
                }
            };
        }
    }
}
