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
    public class MemberManageService
    {
        PageTechsLMSDbContext dbContext;
        public MemberManageService(PageTechsLMSDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<List<MemberInfo>> GetMemberInfoList(string name, int page = 1, int size = 15)
        {
            var query = dbContext.MemberInfos.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name == name);
            }
            query = query.Skip((page - 1) * size).Take(size);

            return await query.ToListAsync();
        }
    }
}
