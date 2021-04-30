using Microsoft.EntityFrameworkCore;
using PageTechsLMS.DataCore.Courses;
using PageTechsLMS.DataCore.DbContexts;
using PageTechsLMS.DataCore.MemberCourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.Service.Courses
{
    public class CourseLearnService
    {
        PageTechsLMSDbContext dbContext;
        public CourseLearnService(PageTechsLMSDbContext _dbContext)
        {
            dbContext = _dbContext;
        }



        public async Task<(List<Course>, List<Lesson>, List<Section>, List<SectionItem>)> GetMyCourseList(string memberId, int page = 1, int size = 5)
        {
            var courseMembers = dbContext.MemberCourseLearnLogs
                  .Where(x => x.MemberId == memberId)
                  .OrderByDescending(x => x.CreateTime)
                  .Skip((page - 1) * size)
                  .Take(size);
            //.ToListAsync();

            List<Course> course = null;
            List<Lesson> lastLesson = null;
            List<Section> lastSection = null;
            List<SectionItem> lastSectionItem = null;
            if (courseMembers != null)
            {
                course = await dbContext.Courses.Where(x => courseMembers.Any(c => c.CourseId == x.Id)).ToListAsync();
                lastLesson = await dbContext.Lessons.Where(x => courseMembers.Any(c => c.LessonId == x.Id)).ToListAsync();
                lastSection = await dbContext.Sections.Where(x => courseMembers.Any(c => c.SectionId == x.Id)).ToListAsync();
                lastSectionItem = await dbContext.SectionItems.Where(x => courseMembers.Any(c => c.SectionItemId == x.Id)).ToListAsync();
            }
            return (course, lastLesson, lastSection, lastSectionItem);
        }

        public async Task StartCourse(Guid courseId, Guid lessonId, Guid sectionId, Guid sectionItemId, string memberId, int Remaining)
        {
            var hasLog = await dbContext.MemberCourseLearnLogs.AnyAsync(x => x.CourseId == courseId && x.LessonId == lessonId && x.SectionId == sectionId && x.SectionItemId == sectionItemId && x.MemberId == memberId);
            if (!hasLog)
            {
                await dbContext.AddAsync(new MemberCourseLearnLog
                {
                    CourseId = courseId,
                    LessonId = lessonId,
                    SectionId = sectionId,
                    SectionItemId = sectionItemId,
                    CreateTime = DateTime.Now,
                    MemberId = memberId,
                    Remaining = Remaining
                });
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task ResumeCourse(Guid courseId, string memberId)
        {

        }
    }
}
