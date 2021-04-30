using Newtonsoft.Json;
using Pagetechs.Framework.Utilities.Https;
using System;
using System.Threading.Tasks;

namespace Pagetechs.Framework.Wx.WebApp
{
    public class WebAppAutherize
    {
        private static string WxLoginUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=[appId]&redirect_uri=[redirectUrl]&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";

        public static string WxLoginGotoUrl(string appId, string redirectUrl)
        {
            return WxLoginUrl.Replace("[appId]", appId).Replace("[redirectUrl]", redirectUrl);
        }

        public static async Task<(UserAccessTokenResult, string)> GetUserAccessToken(string appId, string appsecret, string code)
        {
            Console.WriteLine("====================appId string=====================:" + appId + " :,,, " + appsecret);

            var resultStr = await HttpRequest.Get($"http://api.weixin.qq.com/sns/oauth2/access_token?appid={appId}&secret={appsecret}&code={code}&grant_type=authorization_code");
            Console.WriteLine("====================Result=====================:" + resultStr);
            UserAccessTokenResult jsonObj;
            try
            {
                 jsonObj = JsonConvert.DeserializeObject<UserAccessTokenResult>(resultStr);
            }
            catch (Exception exc)
            {
                 
            }
            return (JsonConvert.DeserializeObject<UserAccessTokenResult>(resultStr), resultStr);
        }

        public static async Task<UserAccessTokenResult> RefreshUserAccessToken(string appId, string refreshtoken)
        {
            var resultStr = await HttpRequest.Get($"https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={appId}&grant_type=refresh_token&refresh_token={refreshtoken}");
            return JsonConvert.DeserializeObject<UserAccessTokenResult>(resultStr);
        }

        public static async Task<WebAppAuthUserInfo> GetUserInfo(string accessToken, string openId)
        {
            var resultStr = await HttpRequest.Get($"https://api.weixin.qq.com/sns/userinfo?access_token={accessToken}&openid={openId}&lang=zh_CN");
            return JsonConvert.DeserializeObject<WebAppAuthUserInfo>(resultStr);
        }
    }
}
