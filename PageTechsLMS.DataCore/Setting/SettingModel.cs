using Newtonsoft.Json; 
using PageTechsLMS.DataCore.Setting.ClientAuth;
using PageTechsLMS.DataCore.Setting.File;
using PageTechsLMS.DataCore.Setting.Message;
using PageTechsLMS.DataCore.Setting.Wx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Setting
{
    public class SettingModel
    { 
        public WxMPSetting WxMP { get; set; } 
        public WxMiniAppSetting WxMiniApp { get; set; }
        public WxPaySetting WxPay { get; set; } 
        public LoginSetting Login { get; set; } 
        public FileSetting File { get; set; } 
        public SMSSetting SMS { get; set; } 
    }
}
