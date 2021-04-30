using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PageTechsLMS.Admin;
using PageTechsLMS.Admin.DbContexts;
using PageTechsLMS.DataCore.AdminUsers;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Oryx.FastAdmin.Antd.IdentityServer.Middleware
{
    public static class IdentityServer4ServicenExtension
    {
        public static IServiceCollection AddIdneityServer(this IServiceCollection services)
        {

            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var dataType = configuration["Infrastructure:IdentityServer4:Database"];

            var aspIdUser = string.Empty;
            var is4Connection = string.Empty;

            switch (dataType)
            {
                case "MssqlDB":
                    aspIdUser = configuration["Infrastructure:IdentityServer4:msSql:aspIdUser"];
                    is4Connection = configuration["Infrastructure:IdentityServer4:msSql:Identity"];
                    break;
                case "MysqlDB":
                    aspIdUser = configuration["Infrastructure:IdentityServer4:mySql:aspIdUser"];
                    is4Connection = configuration["Infrastructure:IdentityServer4:mySql:Identity"];
                    break;
                case "Sqlite":
                default:
                    aspIdUser = configuration["Infrastructure:IdentityServer4:sqlite:aspIdUser"];
                    is4Connection = configuration["Infrastructure:IdentityServer4:sqlite:Identity"];
                    break;
            }
            var authorityHost = configuration["Infrastructure:IdentityServer4:Authority"];

            //var wechatAppId = configuration["Infrastructure:external:wechat:appId"];
            //var wechatAppSecretn = configuration["Infrastructure:external:wechat:appsecret"];

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            var rsaProvider = new RSACryptoServiceProvider();
            SecurityKey key = new RsaSecurityKey(rsaProvider);

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha512);

            #region Identity Server


            switch (dataType)
            {
                case "MssqlDB":
                    services.AddDbContextPool<PTAuthenticationDbContext>(options =>
                            options.UseSqlServer(aspIdUser));
                    break;
                case "MysqlDB":
                    services.AddDbContextPool<PTAuthenticationDbContext>(options =>
                    {
                        options.UseMySql(aspIdUser, ServerVersion.AutoDetect(aspIdUser), mysqlOption =>
                         {
                             mysqlOption.CharSet(CharSet.Utf8);
                         });
                    });
                    break;
                case "Sqlite":
                default:
                    services.AddDbContextPool<PTAuthenticationDbContext>(options =>
                            options.UseSqlite(aspIdUser));
                    break;
            }

            //此处,因数据上下文的缘故,不进行用户数据隔离,只进行权限隔离
            services.AddIdentity<PTUserEntity, PTRoleEntity>()
                    .AddRoleManager<RoleManager<PTRoleEntity>>()
                    //.AddClaimsPrincipalFactory<IUserClaimsPrincipalFactory<SaasAdminAccountEntity>>()
                    .AddEntityFrameworkStores<PTAuthenticationDbContext>()
            .AddDefaultTokenProviders();

            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            //可以作为IdentityServer 进行认证
            services.AddIdentityServer(options =>
            {
                //options.PublicOrigin = authorityHost;
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
               //.AddSigningCredential(signingCredentials)
               .AddDeveloperSigningCredential()
               .AddAspNetIdentity<PTUserEntity>()
               .AddConfigurationStore(options =>
               {
                   switch (dataType)


                   {
                       case "MssqlDB":
                           options.ConfigureDbContext = b => b.UseSqlServer(is4Connection,
                         sql => sql.MigrationsAssembly(migrationsAssembly));
                           break;
                       case "MysqlDB":
                           options.ConfigureDbContext = b => b.UseMySql(is4Connection, ServerVersion.AutoDetect(is4Connection),
                                   sql =>
                                   {
                                       sql.MigrationsAssembly(migrationsAssembly);
                                       sql.CharSet(CharSet.Utf8);
                                   });
                           break;
                       case "Sqlite":
                       default:
                           options.ConfigureDbContext = b => b.UseSqlite(is4Connection,
                                   sql => sql.MigrationsAssembly(migrationsAssembly));
                           break;
                   }
               })
               .AddOperationalStore(options =>
               {
                   switch (dataType)
                   {
                       case "MssqlDB":
                           options.ConfigureDbContext = b => b.UseSqlServer(is4Connection,
                         sql => sql.MigrationsAssembly(migrationsAssembly));
                           break;
                       case "MysqlDB":
                           options.ConfigureDbContext = b => b.UseMySql(is4Connection, ServerVersion.AutoDetect(is4Connection),
                                   sql => sql.MigrationsAssembly(migrationsAssembly)
                                   .CharSet(CharSet.Utf8));
                           break;
                       case "Sqlite":
                       default:
                           options.ConfigureDbContext = b => b.UseSqlite(is4Connection,
                                   sql => sql.MigrationsAssembly(migrationsAssembly));
                           break;
                   }
               })
               ;

            #endregion

            var serviceProvider = services.BuildServiceProvider();
            var _loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            var cors = new DefaultCorsPolicyService(_loggerFactory.CreateLogger<DefaultCorsPolicyService>())
            {
                AllowAll = true
            };
            services.AddSingleton<ICorsPolicyService>(cors);

            /*The EF DbContext Init in Middleware*/

            #region Api Config
            //设置web端的认证资源 ,资源名 
            //后续将资源及认证分离
            //配置web端访问接口
            //services.AddAuthentication("Bearer")
            //.AddIdentityServerAuthentication(options =>
            //{
            //    options.Authority = authorityHost;
            //    options.RequireHttpsMetadata = false;
            //    options.ApiName = "JsPortalResource";
            //});


            #endregion


            #region Mvc Client

            //设置mvc的认证资源, 资源名 
            //可以作为mvc client访问自己
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            //.AddIdentityServerAuthentication(options =>
            //{
            //    options.Authority = authorityHost;
            //    options.RequireHttpsMetadata = false;
            //    options.ApiName = "JsPortalResource";
            //})
            .AddCookie("Cookies")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = authorityHost;
                options.RequireHttpsMetadata = false;
                options.Audience = "MainPortal";
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async ctx =>
                    {

                    },
                    OnMessageReceived = ctx =>
                    {
                        var accessToken = ctx.Request.Query["access_token"];
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            ctx.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "sub"
                };

            })
            //.AddWeChat(options =>
            //{
            //    options.AppId = "default";
            //    options.AppSecret = "default";
            //    options.UseCachedStateDataFormat = true;
            //    options.SaveTokens = true;
            //})
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = authorityHost;
                options.RequireHttpsMetadata = false;

                options.ClientId = "mvcIn";
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                options.UsePkce = true;

                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;


                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("JsPortalResource");
                options.Scope.Add("offline_access");

                options.ClaimActions.MapJsonKey("website", "website");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });
            #endregion

            return services;
        }
    }
}
