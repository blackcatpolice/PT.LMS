using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PagetechsLMS.WebAndApi.IdentityExtension;
using PageTechsLMS.DataCore.DbContexts;
using PageTechsLMS.DataCore.Members;
using PageTechsLMS.Service;
using PageTechsLMS.Service.Wx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagetechsLMS.WebAndApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.SameSite = SameSiteMode.Unspecified;
                options.Cookie.IsEssential = true;
                options.Cookie.Path = "/";
            });

            services.AddLMSService();

            services
                .AddDatabaseDeveloperPageExceptionFilter();

            //        services.AddDbContext<PageTechsLMSDbContext>(options =>
            //options.UseSqlServer(
            //    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            #region Web Auth


            //services.AddDefaultIdentity<MemberAccount>(options => options.SignIn.RequireConfirmedAccount = false)
            //    .AddUserManager<UserManager<MemberAccount>>()
            //    .AddSignInManager<SignInManager<MemberAccount>>()
            //    .AddEntityFrameworkStores<PageTechsLMSDbContext>()
            //      .AddDefaultTokenProviders();

            services.AddIdentityCore<MemberAccount>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
              .AddRoles<MemberRole>()
              .AddEntityFrameworkStores<PageTechsLMSDbContext>()
              .AddSignInManager()
              .AddDefaultTokenProviders();

            #endregion



            #region API Auth

            services.AddIdentityServer()
            .AddSigningCredentials()
            .AddResourceValidator<ResourceValidator>()
            .AddCustomTokenRequestValidator<TokenRequestValidtor>()
            .AddApiAuthorization<MemberAccount, PageTechsLMSDbContext>(options =>
            {
                var client = options.Clients[0];

                client.ClientSecrets.Add(new Secret("secret".Sha256()));
                client.AllowOfflineAccess = true;
                client.AllowedGrantTypes.Add("password");

                client.AllowedScopes.Add("openid,profile,PagetechsLMS.WebAndApiAPI,offline_access");
                client.AllowedScopes.Add("openid");
                client.AllowedScopes.Add("profile");
                client.AllowedScopes.Add("PagetechsLMS.WebAndApiAPI");
                client.AllowedScopes.Add("offline_access");

                client.UpdateAccessTokenClaimsOnRefresh = true;
                client.RefreshTokenExpiration = TokenExpiration.Sliding;
                client.RefreshTokenUsage = TokenUsage.OneTimeOnly;

                var resource = options.IdentityResources;


                var api = options.ApiResources;
                api.Remove(api[0]);
                api.Add(new ApiResource { Name = "PagetechsLMS.WebAndApiAPI", Scopes = new List<string> { "PagetechsLMS.WebAndApiAPI" } });

                var scopes = options.ApiScopes;
                scopes.Add(new ApiScope
                {
                    Name = "PagetechsLMS.WebAndApiAPI",
                    DisplayName = "PagetechsLMS.WebAndApiAPI",
                    Enabled = true
                });
            });


            services.AddAuthentication()
            .AddIdentityServerJwt();

            ///cookie 和 jwt登录,就这个顺序,千万不要动!
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityConstants.ApplicationScheme;
                o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })

            .AddIdentityCookies(o => { });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.IsEssential = true;

            });



            //services.Configure<SessionOptions>(options =>
            //{
            //    options.Cookie.SameSite = SameSiteMode.Unspecified;
            //    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            //    options.Cookie.IsEssential = true;
            //});

            //services.Configure<CookieAuthenticationOptions>("idsrv.session", options =>
            //{
            //    options.Cookie.SameSite = SameSiteMode.Unspecified;
            //    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            //    options.Cookie.IsEssential = true;
            //}); 


            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.OnAppendCookie = cookieContext =>
                {
                    cookieContext.CookieOptions.SameSite = SameSiteMode.Unspecified;
                    cookieContext.CookieOptions.Secure = true;
                    cookieContext.CookieOptions.IsEssential = true;
                    cookieContext.CookieOptions.Path = "/";
                };
                options.Secure = CookieSecurePolicy.SameAsRequest;


                options.OnDeleteCookie = cookieContext =>
                {
                    cookieContext.CookieOptions.SameSite = SameSiteMode.Unspecified;
                    cookieContext.CookieOptions.Secure = true;
                    cookieContext.CookieOptions.IsEssential = true;
                    cookieContext.CookieOptions.Path = "/";
                };
            });
            #endregion

            //services.AddControllersWithViews();
            services.AddRazorPages().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseStatusCodePagesWithRedirects("/Error");


            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            var physicpath = AppDomain.CurrentDomain.BaseDirectory + "/Uploads";
            if (!Directory.Exists(physicpath))
            {
                Directory.CreateDirectory(physicpath);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/Uploads",
                FileProvider = new PhysicalFileProvider(physicpath)
            });

            app.UseRouting();
            app.UseCookiePolicy();

            app.UseSession();

            app.UseMPAuth();
            app.UseWxPayProccessor();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "area",
                    areaName: "Admin",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
