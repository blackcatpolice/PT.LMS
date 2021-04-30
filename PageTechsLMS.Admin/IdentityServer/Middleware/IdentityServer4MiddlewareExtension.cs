using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oryx.FastAdmin.Antd.IdentityServer.Model;
using PageTechsLMS.Admin.DbContexts;
using PageTechsLMS.DataCore.AdminUsers;
using System.Collections.Generic;
using System.Linq;

namespace Oryx.FastAdmin.Antd.IdentityServer.Middleware
{
    public static class IdentityServer4MiddlewareExtension
    {
        public static IApplicationBuilder UserIS4(this IApplicationBuilder app)
        {
            app.InitializeIS4Database();
            app.UseIdentityServer();

            return app;
        }

        public static IApplicationBuilder InitializeIS4Database(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<PTUserEntity>>();

                var roleMgr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<PTRoleEntity>>();

                serviceScope.ServiceProvider.GetRequiredService<PTAuthenticationDbContext>()
                    .Database.Migrate();

                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>()
                    .Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();

                var configuration = app.ApplicationServices.GetService<IConfiguration>();

                //ConfigUserAndRole(userMgr, roleMgr, configuration);

                //ConfigIdentityServer(context, configuration);
            }

            return app;
        }

        private static void ConfigUserAndRole(UserManager<PTUserEntity> userMgr, RoleManager<PTRoleEntity> roleMgr, IConfiguration configuration)
        {
            var users = configuration.GetSection("Users").Get<List<UserConfigOptions>>();
            var roles = configuration.GetSection("Roles").Get<string[]>();
            foreach (var roleItem in roles)
            {
                var role = roleMgr.FindByNameAsync(roleItem).Result;
                if (role == null)
                {
                    roleMgr.CreateAsync(new PTRoleEntity { Name = roleItem }).Wait();
                }
            }
            foreach (var userItem in users)
            {
                var user = userMgr.FindByNameAsync(userItem.UserName).Result;
                if (user == null)
                {
                    var userEntity = new PTUserEntity { UserName = userItem.UserName };
                    var result = userMgr.CreateAsync(userEntity, userItem.Password).Result;
                    if (result.Succeeded)
                    {
                        userMgr.AddToRolesAsync(userEntity, userItem.Roles.ToList()).Wait();
                    }
                }
            }
        }

        private static void ConfigIdentityServer(ConfigurationDbContext context, IConfiguration configuration)
        {
            var ClientsOptions = configuration.GetSection("Clients").Get<List<ClientConfigOperation>>();

            var ApiResourceOptions = configuration.GetSection("ApiResources").Get<List<ApiResourceConfigOperation>>();

            var IdentityResourceOPtions = configuration.GetSection("IdentityResource").Get<List<IdentityResource>>();

            if (ClientsOptions != null)
            {
                var clients = context.Clients;
                context.RemoveRange(clients);
                context.SaveChanges();
                foreach (var client in ClientsOptions)
                {
                    if (!context.Clients.Any(x => x.ClientId == client.ClientId))
                    {

                        var clientEntity = new Client()
                        {
                            ClientId = client.ClientId,
                            ClientName = client.ClientName,
                            ClientSecrets = { new Secret(client.ClientSecrets[0].Sha256()) },
                            AllowOfflineAccess = true,
                            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                            AllowedCorsOrigins = client.AllowedCorsOrigins,
                            AllowedScopes = client.AllowedScopes,
                            RedirectUris = client.RedirectUris,
                            PostLogoutRedirectUris = client.PostLogoutRedirectUris,
                            UpdateAccessTokenClaimsOnRefresh = true,
                            RefreshTokenExpiration = TokenExpiration.Sliding,
                            RefreshTokenUsage = TokenUsage.OneTimeOnly,


                        };
                        context.Clients.Add(clientEntity.ToEntity());
                    }
                }
                context.SaveChanges();
            }
            //#region Test Code
            //if (!context.Clients.Any(x => x.ClientId == "ro.client"))
            //{
            //    var clientEntity2 = new Client()
            //    {
            //        ClientId = "ro.client",
            //        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            //        ClientSecrets =
            //                    {
            //                        new Secret("secret".Sha256())
            //                    },
            //        AllowedScopes = { "profile", "openid", "email", "role" }
            //    };
            //    context.Clients.Add(clientEntity2.ToEntity());
            //    context.SaveChanges();
            //}

            //#endregion

            foreach (var resource in Ids)
            {
                if (!context.IdentityResources.Any(x => x.Name == resource.Name))
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
            }
            if (IdentityResourceOPtions != null)
            {
                foreach (var resource in IdentityResourceOPtions)
                {
                    if (!context.IdentityResources.Any(x => x.Name == resource.Name))
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                }
            }
            context.SaveChanges();


            if (ApiResourceOptions != null)
            {
                var apiResources = context.ApiResources;
                context.RemoveRange(apiResources);
                foreach (var resource in ApiResourceOptions)
                {
                    if (!context.ApiResources.Any(x => x.Name == resource.Name))
                    {
                        var resourceEntity = new ApiResource(resource.Name);

                        var scopes = resource.Scope.Split(' ');
                        foreach (var scope in scopes)
                        {
                            resourceEntity.Scopes.Add(scope);
                        }
                        resourceEntity.UserClaims.Add(JwtClaimTypes.Audience);
                        context.ApiResources.Add(resourceEntity.ToEntity());
                    }
                }
                context.SaveChanges();
            }


            if (!context.ApiScopes.Any(x => x.Name == "MainPortal"))
            {
                context.ApiScopes.Add(new ApiScope
                {
                    Name = "MainPortal",
                    DisplayName = "MainPortal",
                    Enabled = true
                }.ToEntity());
                context.SaveChanges();
            }
        }


        static IEnumerable<IdentityResource> Ids =>
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
