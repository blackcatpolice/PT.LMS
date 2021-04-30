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
    public class CourseManageService
    {
        PageTechsLMSDbContext dbContext;
        public CourseManageService(PageTechsLMSDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<List<Category>> GetCategoryAsync(int page = 1, int size = 15)
        {
            return await dbContext.Category.Skip((page - 1) * size).Take(size).ToListAsync();
        }
        public async Task<(List<Course>, int)> GetCoursesAsync(int page = 1, int size = 15)
        {
            var total = await dbContext.Courses.CountAsync();
            return (await dbContext.Courses.Skip((page - 1) * size).Take(size).ToListAsync(), total);
        }
        public async Task<List<Lesson>> GetLessonsAsync(Guid courseId, int page = 1, int size = 15)
        {
            return await dbContext.Lessons.Where(x => x.CourseId == courseId).Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task<List<Section>> GetSectionsAsync(Guid lessonId, int page = 1, int size = 15)
        {
            return await dbContext.Sections.Where(x => x.LessonId == lessonId).Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task<List<SectionItem>> GetSectionItemsAsync(Guid sectionId, int page = 1, int size = 15)
        {
            return await dbContext.SectionItems.Where(x => x.SectionId == sectionId).Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task<Category> GetCategoryDetailAsync(Guid courseId)
        {
            return await dbContext.Category.FirstOrDefaultAsync(x => x.Id == courseId);
        }

        public async Task<Course> GetCourseDetailAsync(Guid courseId)
        {
            return await dbContext.Courses.Include("Tags").FirstOrDefaultAsync(x => x.Id == courseId);
        }
        public async Task<Lesson> GetLessonDetailAsync(Guid lessonId)
        {
            return await dbContext.Lessons.FirstOrDefaultAsync(x => x.Id == lessonId);
        }
        public async Task<Section> GetSectionDetailAsync(Guid sectionId)
        {
            return await dbContext.Sections.FirstOrDefaultAsync(x => x.Id == sectionId);
        }
        public async Task<SectionItem> GetSectionItemDetailAsync(Guid sectionItemId)
        {
            return await dbContext.SectionItems.FirstOrDefaultAsync(x => x.Id == sectionItemId);
        }

        public async Task AddOrUpdateCategoryAsync(Category category)
        {
            if (category.Id == Guid.Empty)
            {
                category.Id = Guid.NewGuid();
                await dbContext.AddAsync(category);
            }
            else
            {
                dbContext.Update(category);
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task AddOrUpdateCourseAsync(Course course)
        {
            
            if (course.Id == Guid.Empty)
            {
                course.Id = Guid.NewGuid();
                await dbContext.AddAsync(course);
            }
            else
            {    
                dbContext.Update(course);
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task AddOrUpdateLessonAsync(Lesson lesson)
        {
            if (lesson.Id == Guid.Empty)
            {
                lesson.Id = Guid.NewGuid();
                await dbContext.AddAsync(lesson);
            }
            else
            {
                dbContext.Update(lesson);
            }
            await dbContext.SaveChangesAsync();
        }
        public async Task AddOrUpdateSectionAsync(Section section)
        {
            if (section.Id == Guid.Empty)
            {
                section.Id = Guid.NewGuid();

                await dbContext.AddAsync(section);
            }
            else
            {
                dbContext.Update(section);
            }
            await dbContext.SaveChangesAsync();
        }
        public async Task AddOrUpdateSectionItemAsync(SectionItem sectionItem)
        {
            if (sectionItem.Id == Guid.Empty)
            {
                sectionItem.Id = Guid.NewGuid();
                await dbContext.AddAsync(sectionItem);
            }
            else
            {
                dbContext.Update(sectionItem);
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            var category = await dbContext.Category.FirstOrDefaultAsync(x => x.Id == categoryId);
            dbContext.Category.Remove(category);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(Guid courseId)
        {
            var course = await dbContext.Courses.FirstOrDefaultAsync(x => x.Id == courseId);
            dbContext.Courses.Remove(course);
            await dbContext.SaveChangesAsync();
        }
        public async Task DeleteLessonAsync(Guid lessonId)
        {
            var lesson = await dbContext.Lessons.FirstOrDefaultAsync(x => x.Id == lessonId);
            dbContext.Lessons.Remove(lesson);
            await dbContext.SaveChangesAsync();
        }
        public async Task DeleteSectionAsync(Guid setionId)
        {
            var course = await dbContext.Sections.FirstOrDefaultAsync(x => x.Id == setionId);
            dbContext.Sections.Remove(course);
            await dbContext.SaveChangesAsync();
        }
        public async Task DeleteSectionItemAsync(Guid sectionItemid)
        {
            var sectionItem = await dbContext.SectionItems.FirstOrDefaultAsync(x => x.Id == sectionItemid);
            dbContext.SectionItems.Remove(sectionItem);
            await dbContext.SaveChangesAsync();
        }
    }
}
