using PageTechsLMS.DataCore.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.Service.Member
{
    public class MemberAuthService
    {
        PageTechsLMSDbContext dbContext;
        public MemberAuthService(PageTechsLMSDbContext _dbContext)
        {
            dbContext = _dbContext;
        }


        public async Task SendEmailCode()
        {

        }
        public async Task SendSMSCode()
        {

        }

        public async Task GenerateImgCode()
        {

        }

        public async Task LoginWithEmail()
        {

        }

        public async Task LoginWithPhone()
        {

        }

        public async Task LoginWithWechat()
        {
             
        }

        public async Task LoginWithWeApp()
        {

        }
    }
}
