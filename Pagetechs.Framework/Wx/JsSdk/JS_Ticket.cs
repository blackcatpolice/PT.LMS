using Pagetechs.Framework.Utilities.Https;
using Pagetechs.Framework.Wx.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagetechs.Framework.Wx.JsSdk
{
    public class Jsapi_Ticket
    {
        private WxAccessToken _accessToken;
        public Jsapi_Ticket(WxAccessToken accessToken)
        {
            _accessToken = accessToken;
        }
        public async Task<string> Get()
        {
            return await HttpRequest.Get("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + _accessToken.GetAccessToken() + "&type=jsapi");
        }

    }
}
