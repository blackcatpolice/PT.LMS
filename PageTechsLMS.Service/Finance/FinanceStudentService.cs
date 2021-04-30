using PageTechsLMS.DataCore.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.Service.Finance
{
    public class FinanceStudentService
    {
        PageTechsLMSDbContext dbContext;
        public FinanceStudentService(PageTechsLMSDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
    }
}
