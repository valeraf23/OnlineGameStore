using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

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
                new IdentityResource("roles", "Your role(s)", new []{"role"})
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new []
            {
                new ApiResource("onlinegamestoreapi", "online game store api", new []{"role","name"})
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client {
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
                    RedirectUris = {"https://localhost:4200/assets/signin-oidc.html","https://localhost:4200/assets/silent-redirect.html"},
                    PostLogoutRedirectUris =  {"https://localhost:4200/?postLogout=true"},
                    IdentityTokenLifetime=120,
                    AccessTokenLifetime=120
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "fec0a4d6-5830-4eb8-8024-272bd5d6d2bb",
                    Username = "admin",
                    Password = "qwerty",
                    Claims = new List<Claim>
                    {
                        new Claim("name", "admin"),
                        new Claim("role", "Administrator")
                    }
                }
            };
        }
    }
}
