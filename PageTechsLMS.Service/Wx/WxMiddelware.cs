
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Abstractions;
using Microsoft.AspNetCore.Builder;
using System.Web;
using Microsoft.AspNetCore.Identity;
using PageTechsLMS.DataCore.Members;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Pagetechs.Framework.Wx.WebApp;
using System.Security.Claims;

namespace PageTechsLMS.Service.Wx
{
    public static class WxMiddelware
    {
        public static IApplicationBuilder UseMPAuth(this IApplicationBuilder app)
        {
            app.MapWhen((ctx) =>
          {

              var accessTokenJson = ctx.Session.GetString("AccessToken");

              var accessToken = JsonConvert.DeserializeObject<UserAccessTokenResult>(accessTokenJson ?? "");

              var wxService = ctx.RequestServices.GetService<WxService>();
              return ctx.Request.Headers["User-Agent"].Any(x => x.ToLower().Contains("micromessenger"))
              && !ctx.Request.Path.Value.Contains("/UrlWxMPLogin")
              && !(ctx.User.Identity.IsAuthenticated || !string.IsNullOrEmpty(accessToken?.OpenId));
          }, appbuilder =>
          {
              appbuilder.Run(async ctx =>
              {
                  var wxService = ctx.RequestServices.GetService<WxService>();
                  var callbackUrl = ctx.Request.Scheme + "://" + ctx.Request.Host + "/UrlWxMPLogin";
                  var redirectUrl = wxService.GetRedirectUrl(HttpUtility.UrlEncode(callbackUrl));
                  ctx.Response.Redirect(redirectUrl);
                  await Task.CompletedTask;
              });

          });

            app.Map("/UrlWxMPLogin", appBuilder =>
            {
                appBuilder.Run(async ctx =>
                {
                    var wxService = appBuilder.ApplicationServices.GetService<WxService>();
                    var signInManager = ctx.RequestServices.GetService<SignInManager<MemberAccount>>();
                    var userManager = ctx.RequestServices.GetService<UserManager<MemberAccount>>();
                    var logger = ctx.RequestServices.GetService<ILogger<WxService>>();
                    try
                    {
                        var code = ctx.Request.Query["code"];
                        var UserAccessTokenResult = await wxService.GetMPAccessToken(code);
                        ctx.Session.SetString("AccessToken", JsonConvert.SerializeObject(UserAccessTokenResult));

                        var userInfo = await wxService.GetUserInfo(UserAccessTokenResult.AccessToken, UserAccessTokenResult.OpenId);

                        var hasUser = await userManager.FindByLoginAsync("wxMP", UserAccessTokenResult.OpenId);
                        logger.LogInformation("Has user");
                        if (hasUser == null)
                        {
                            var user = new MemberAccount
                            {
                                UserName = "Wechat-" + Guid.NewGuid().ToString(),
                                NormalizedUserName = userInfo.NickName,
                                NickName = userInfo.NickName,
                                MemberBind = new MemberBind
                                {
                                    WxNickName = userInfo.NickName,
                                    WxOpenId = userInfo.OpenId
                                }
                            };
                            await userManager.CreateAsync(user, "WechatPassword123#");
                            //await userManager.AddLoginAsync(user, new UserLoginInfo("wxMP", UserAccessTokenResult.OpenId, "Wechat"));
                            await userManager.AddClaimAsync(user, new Claim("wxopenid", UserAccessTokenResult.OpenId));
                            await userManager.AddClaimAsync(user, new Claim("nickName", userInfo.NickName));

                            await signInManager.SignInAsync(user, true);
                        }
                        else
                        {
                            logger.LogInformation("SignIn user");
                            var claims = await userManager.GetClaimsAsync(hasUser);
                            if (!claims.Any(x => x.Type == "wxopenid"))
                            {
                                await userManager.AddClaimAsync(hasUser, new Claim("wxopenid", UserAccessTokenResult.OpenId));
                            }
                            if (!claims.Any(x => x.Type == "nickName"))
                            {
                                await userManager.AddClaimAsync(hasUser, new Claim("nickName", userInfo.NickName));
                            }
                            await signInManager.SignInAsync(hasUser, true);
                        }

                        ctx.Response.Redirect("/");
                    }
                    catch (Exception exc)
                    {
                        logger.LogError(exc, exc.Message);
                    }
                });
            });

            return app;
        }
    }
}
