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
    public class CourseExploreService
    {
        PageTechsLMSDbContext dbContext;
        public CourseExploreService(PageTechsLMSDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<Course> GetCourseAsync(Guid courseId)
        {
            return await dbContext.Courses.FirstOrDefaultAsync(x => x.Id == courseId);
        }

        public async Task<List<Category>> GetCategoryList(string name)
        {
            var query = dbContext.Category.AsQueryable();
            if (name != null)
            {
                query = query.Where(x => x.Name == name);
            }
            return await query.ToListAsync();
        }

        public async Task<List<Course>> GetAllCourseList()
        {
            return await dbContext.Courses
                .ToListAsync();
        }

        public async Task<List<DataCore.Courses.Course>> GetCourseByCategory(Guid cateId, int page = 1, int size = 5)
        {
            return await dbContext.Courses
                .Where(x => x.CategoryId == cateId)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<List<DataCore.Courses.Course>> GetHotCourse()
        {
            return await dbContext.Courses
                .Where(x => x.IsHot)
                .OrderByDescending(x => x.Order)
                .OrderByDescending(x => x.CreateTime)
                .Take(5)
                .ToListAsync();
        }

        public async Task<List<Course>> GetRecommanded(Guid? courseId, Guid? cateId)
        {
            var targetCourse = await dbContext.Courses.FirstOrDefaultAsync(x => x.Id == courseId);

            return await dbContext.Courses
                   .Where(x => x.Tags.Any(c => targetCourse.Tags.Any(v => v.Name == c.Name)))
                   .OrderByDescending(x => x.Order)
                   .OrderByDescending(x => x.CreateTime)
                   .Take(5)
                   .ToListAsync();
        }

        public async Task<(Course, MemberCourseLearnLog, MemberCoursePayLog)> GetCourseDetailWithMemberLog(Guid courseId, string memberId)
        {
            var course = await dbContext.Courses.FirstOrDefaultAsync(x => x.Id == courseId);

            var courseLearnLog = memberId != null ? await dbContext.MemberCourseLearnLogs.FirstOrDefaultAsync(x => x.CourseId == courseId && x.MemberId == memberId) : null;
            var coursePayLog = memberId != null ? await dbContext.MemberCoursePayLogs.FirstOrDefaultAsync(x => x.CourseId == courseId && x.MemberId == memberId) : null;
            return (course, courseLearnLog, coursePayLog);
        }


        public async Task<Course> ViewCourse(Guid courseId)
        {
            var course = await dbContext.Courses.FirstOrDefaultAsync(x => x.Id == courseId);
            course.ViewNumb += 1;
            dbContext.Courses.Update(course);
            await dbContext.SaveChangesAsync();
            return course;
        }

        public async Task<(Course, SectionItem)> GetTrailer(Guid courseId)
        {
            var course = await dbContext.Courses.FirstOrDefaultAsync(x => x.Id == courseId);
            var trailer = await dbContext.SectionItems.FirstOrDefaultAsync(x => x.CourseId == courseId && x.IsTrailer);
            return (course, trailer);
        }

        #region Social Operation

        public async Task LikeCourse(Guid courseId, string memberId)
        {
            var course = await dbContext.Courses.FirstOrDefaultAsync(x => x.Id == courseId);
            var courseFeedback = await getFeedbackData(courseId, memberId);

            courseFeedback.IsLiked = true;
            course.Like += 1;

            await saveFeedback(courseFeedback, course);
        }

        public async Task UnLikeCourse(Guid courseId, string memberId)
        {
            var course = await dbContext.Courses.FirstOrDefaultAsync(x => x.Id == courseId);
            var courseFeedback = await getFeedbackData(courseId, memberId);

            courseFeedback.IsLiked = false;
            course.Like -= 1;

            await saveFeedback(courseFeedback, course);
        }

        public async Task FavoriteCourse(Guid courseId, string memberId)
        {
            var course = await dbContext.Courses.FirstOrDefaultAsync(x => x.Id == courseId);
            var courseFeedback = await getFeedbackData(courseId, memberId);

            courseFeedback.IsFavorite = true;
            course.Favorite += 1;

            await saveFeedback(courseFeedback, course);
        }

        public async Task UnFavoriteCourse(Guid courseId, string memberId)
        {
            var course = await dbContext.Courses.FirstOrDefaultAsync(x => x.Id == courseId);
            var courseFeedback = await getFeedbackData(courseId, memberId);

            courseFeedback.IsLiked = false;
            course.Like -= 1;

            await saveFeedback(courseFeedback, course);
        }


        private async Task saveFeedback(MemberFeedbackLog courseFeedback, Course course)
        {
            if (courseFeedback.Id == Guid.Empty)
            {
                await dbContext.AddAsync(courseFeedback);
            }
            else
            {
                dbContext.Update(courseFeedback);
            }

            dbContext.Update(course);

            await dbContext.SaveChangesAsync();
        }

        private async Task<MemberFeedbackLog> getFeedbackData(Guid courseId, string memberId)
        {
            var courseFeedback = await dbContext.MemberFeedbackLogs.FirstOrDefaultAsync(x => x.CourseId == courseId && x.MemberId == memberId);

            if (courseFeedback == null)
            {
                courseFeedback = new MemberFeedbackLog
                {
                    CourseId = courseId,
                    MemberId = memberId
                };
            }

            return courseFeedback;
        }

        #endregion
    }
}
