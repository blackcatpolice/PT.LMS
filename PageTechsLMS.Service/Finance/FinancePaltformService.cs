using PageTechsLMS.DataCore.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.Service.Finance
{
    public class FinancePaltformService
    {
        PageTechsLMSDbContext dbContext;
        public FinancePaltformService(PageTechsLMSDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
    }
}
