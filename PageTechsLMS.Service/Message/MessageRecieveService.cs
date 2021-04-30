using PageTechsLMS.DataCore.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.Service.Message
{
    public class MessageRecieveService
    {
        PageTechsLMSDbContext dbContext;
        public MessageRecieveService(PageTechsLMSDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
    }
}
