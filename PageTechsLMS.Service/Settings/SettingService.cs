using Microsoft.Extensions.Configuration;
using PageTechsLMS.DataCore.DbContexts; 
using PageTechsLMS.DataCore.Setting;
using PageTechsLMS.DataCore.Setting.Basic;
using PageTechsLMS.DataCore.Setting.ClientAuth;
using PageTechsLMS.DataCore.Setting.File;
using PageTechsLMS.DataCore.Setting.Message;
using PageTechsLMS.DataCore.Setting.Wx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.Service.Settings
{
    public class SettingService
    {
        SettingModel settingModel;
        PageTechsLMSDbContext dbContext;
        public SettingService(IConfiguration _configuration, PageTechsLMSDbContext _dbContext)
        {
            settingModel = _configuration.Get<SettingModel>();
            dbContext = _dbContext;
        }

        public WxMPSetting WxMPSetting => settingModel.WxMP;
        public WxMiniAppSetting WxMiniApp => settingModel.WxMiniApp;
        public WxPaySetting WxPay => settingModel.WxPay;
        public LoginSetting Login => settingModel.Login;
        public FileSetting File => settingModel.File;
        public SMSSetting SMS => settingModel.SMS; 

        public SiteSetting SiteSetting
        {
            get
            {
                return dbContext.SiteSettings.FirstOrDefault();
            }
            set
            {
                var settingModel = dbContext.SiteSettings.FirstOrDefault();
                settingModel.SiteName = value.SiteName;
                settingModel.Description = value.Description;
                settingModel.Keys = value.Keys; 
                settingModel.FooterInfo = value.FooterInfo;
                settingModel.FooterScript = value.FooterScript;  
            }
        }
    }
}
