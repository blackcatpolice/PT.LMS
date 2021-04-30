using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oryx.FastAdmin.Antd.IdentityServer.Model
{
    public static class Config
    {
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("MainPortal", "Portal Resource"){
                 ApiSecrets={
                    new Secret("secret".Sha256())
                 }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "MainPortal" }
                },
                 new Client{
                    ClientId = "mvcIn",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent=false,
                    RequirePkce=true,
                    RedirectUris = { "https://localhost:44337/signin-oidc"},
                    PostLogoutRedirectUris = { "https://localhost:44337/signout-callback-oidc" }, 
                    //client code challenge
                    AllowPlainTextPkce=false,

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "JsPortalResource",
                        "AdminPanel"
                    },
                    AllowOfflineAccess = true
                },
            };

        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResources.Address(),
                new IdentityResource("role", new List<string>(){"roles"})
            };
    }
}
