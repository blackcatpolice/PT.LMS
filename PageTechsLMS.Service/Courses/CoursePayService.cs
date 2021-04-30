using Microsoft.EntityFrameworkCore;
using PageTechsLMS.DataCore.Courses;
using PageTechsLMS.DataCore.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.Service.Courses
{
    public class CoursePayService
    {
        PageTechsLMSDbContext dbContext;
        public CoursePayService(PageTechsLMSDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<Course> GetCourseAsync(Guid courseId)
        {
            return await dbContext.Courses.FirstOrDefaultAsync(x => x.Id == courseId);
        }

    }
}
