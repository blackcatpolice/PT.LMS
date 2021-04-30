using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pagetechs.Framework.Wx.WebApp;
using PageTechsLMS.DataCore.Setting;
using PageTechsLMS.Service.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PageTechsLMS.Service.Wx
{
    public class WxService
    {
        //IConfiguration configuration;
        SettingService settingService;

        //public string MPAccessToken { get; private set; }
        //public UserAccessTokenResult UserAccessTokenResult { get; private set; }
        ILogger<WxService> logger;
        public WxService(SettingService _settingService, ILogger<WxService> _logger)
        {
            logger = _logger;
            settingService = _settingService;
        }


        public string GetRedirectUrl(string redirectUrl) => WebAppAutherize.WxLoginGotoUrl(settingService.WxMPSetting.AppKey, redirectUrl);

        public async Task<UserAccessTokenResult> GetMPAccessToken(string code)
        {
            var appId = settingService.WxMPSetting.AppKey;
            var appsecret = settingService.WxMPSetting.AppSecret;
            var result = await WebAppAutherize.GetUserAccessToken(appId, appsecret, code);
            //UserAccessTokenResult = result.Item1;
            logger.LogInformation("result.Item2================================" + result.Item2);
            //MPAccessToken = UserAccessTokenResult.AccessToken;
            return result.Item1;
        }

        public async Task<string> GenerateMPCode()
        {
            //var setting = configuration.Get<SettingModel>();

            return string.Empty;
        }

        public async Task<string> GenerateMiniAppCode()
        {
            //var setting = configuration.Get<SettingModel>();

            return string.Empty;
        }

        public Task<WebAppAuthUserInfo> GetUserInfo(string accessToken, string openId) => WebAppAutherize.GetUserInfo(accessToken, openId);
    }
}
