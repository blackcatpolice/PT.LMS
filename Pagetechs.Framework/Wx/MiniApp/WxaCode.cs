using Pagetechs.Framework.Utilities.Https;
using Pagetechs.Framework.Wx.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagetechs.Framework.Wx.MiniApp
{
    public class WxaCode
    {
        private WxAccessToken _accessToken;
        public WxaCode(WxAccessToken accessToken)
        {
            _accessToken = accessToken;
        }

        public async Task<Stream> GetWxaCode(string voteType)
        {
            return await HttpRequest.Post($"https://api.weixin.qq.com/wxa/getwxacode?access_token={_accessToken.GetAccessToken()}", new { page = "/page/index/index?type=" + voteType });
        }
    }
}
