using IdentityServer4.Models;
using IdentityServer4;
using Microsoft.AspNetCore.Identity;
using Server.Constants;

namespace Server
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                 new IdentityResources.OpenId(),
                 new IdentityResources.Profile(),
                 new IdentityResources.Phone(),
                 new IdentityResources.Email(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[] {
                new ApiScope("AppAPI.read"),
                new ApiScope("AppAPI.write"),
            };
        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("API")
                {
                    Scopes = new List<string> { "AppAPI.read", "AppAPI.write"},
                    ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                    UserClaims = new List<string> { "role" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientId = "Client",
                    AllowedGrantTypes =new[] {IdentityConstant.GrantType.UserCredentials},
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AccessTokenLifetime = 1,
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes = {
                        "AppAPI.read",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Phone,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    }
                },
                
                new Client
                {
                    ClientId = "Client1",
                    AllowedGrantTypes =new[] {IdentityConstant.GrantType.UserCredentials},
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AccessTokenLifetime = 1209650,
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes = {
                    IdentityServerConstants.StandardScopes.Phone,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    }
                },
            };
    }
}
