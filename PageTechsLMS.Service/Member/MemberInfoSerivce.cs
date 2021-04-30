using Microsoft.EntityFrameworkCore;
using PageTechsLMS.DataCore.DbContexts;
using PageTechsLMS.DataCore.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.Service.Member
{
    public class MemberInfoSerivce
    {
        PageTechsLMSDbContext dbContext;
        public MemberInfoSerivce(PageTechsLMSDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<MemberInfo> GetUserInfo(string memberId)
        {
            return await dbContext.MemberInfos.FirstOrDefaultAsync(x => x.MemberId == memberId);
        }

        public async Task UpdateUserInfo(MemberInfo memberInfo)
        {
            var _memberInfo = await dbContext.MemberInfos.FirstOrDefaultAsync(x => x.MemberId == memberInfo.MemberId);

            if (_memberInfo == null)
            {
                dbContext.Add(memberInfo);
            }
            else
            {
                _memberInfo.Avatart = memberInfo.Avatart;
                _memberInfo.Name = memberInfo.Name;
                dbContext.Update(_memberInfo);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
