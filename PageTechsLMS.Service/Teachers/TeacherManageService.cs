using PageTechsLMS.DataCore.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.Service.Teachers
{
    public class TeacherManageService
    {
        PageTechsLMSDbContext dbContext;
        public TeacherManageService(PageTechsLMSDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
    }
}
