using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PageTechsLMS.DataCore.Members;
using PageTechsLMS.Service.Wx;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace PagetechsLMS.WebAndApi.Pages.Wx
{
    public class UrlWxMPLoginModel : PageModel
    {
        WxService wxService;
        SignInManager<MemberAccount> signInManager;
        UserManager<MemberAccount> userManager;
        ILogger<WxService> logger;
        public UrlWxMPLoginModel(WxService _service,
            SignInManager<MemberAccount> _signInManager,
            UserManager<MemberAccount> _userManager,
            ILogger<WxService> _logger)
        {
            wxService = _service;
            signInManager = _signInManager;
            userManager = _userManager;
            logger = _logger;
        }
        public async Task<ActionResult> OnGet()
        {
            var code = Request.Query["code"];
            var UserAccessTokenResult = await wxService.GetMPAccessToken(code);
            HttpContext.Session.SetString("AccessToken", JsonConvert.SerializeObject(UserAccessTokenResult));

            var userInfo = await wxService.GetUserInfo(UserAccessTokenResult.AccessToken, UserAccessTokenResult.OpenId);

            var hasUser = await userManager.FindByLoginAsync("wxMP", UserAccessTokenResult.OpenId);
            if (hasUser == null)
            {
                var user = new MemberAccount
                {
                    UserName = "Wechat-" + Guid.NewGuid().ToString(),
                    NormalizedUserName = userInfo.NickName
                };

                await userManager.CreateAsync(user, "WechatPassword123#"); 
                await userManager.AddLoginAsync(user, new UserLoginInfo("wxMP", UserAccessTokenResult.OpenId, "Wechat"));
                await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("wxopenid", UserAccessTokenResult.OpenId));

                await signInManager.SignInAsync(user, true);
            }
            else
            {
                await signInManager.SignInAsync(hasUser, true);
            }


            return Redirect("/");

        }
    }
}
