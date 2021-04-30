using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pagetechs.Framework.Dtos;
using PageTechsLMS.Admin.DbContexts;
using PageTechsLMS.DataCore.DbContexts;
using PageTechsLMS.DataCore.Setting;
using PageTechsLMS.DataCore.Setting.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageTechsLMS.Admin.Controllers.Setting
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        PageTechsLMSDbContext dbContext;
        public SettingController(PageTechsLMSDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        [HttpGet]
        public async Task<string> GetConfigSetting()
        {
            var allSet = await System.IO.File.ReadAllTextAsync("./set.config.json");
            return allSet;
        }

        [HttpGet]
        public async Task<ApiMessage> GetSiteSetting()
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await dbContext.SiteSettings.FirstOrDefaultAsync();
            });
            return apiMsg;
        }

        [HttpPost]
        public async Task<ApiMessage> PostSiteSetting(SiteSetting settings)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                if (settings.Id == Guid.Empty)
                {
                    dbContext.Add(settings);
                }
                else
                {
                    dbContext.Update(settings);
                }
                await dbContext.SaveChangesAsync();
            });
            return apiMsg;
        }
    }
}
