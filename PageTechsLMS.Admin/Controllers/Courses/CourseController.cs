using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pagetechs.Framework.Dtos;
using Pagetechs.Framework.Dtos.ModolHelper;
using PageTechsLMS.Admin.Dtos;
using PageTechsLMS.Admin.Dtos.Course;
using PageTechsLMS.DataCore.Courses;
using PageTechsLMS.DataCore.DbContexts;
using PageTechsLMS.Service.Courses;
using PageTechsLMS.Service.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageTechsLMS.Admin.Controllers.Courses
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CourseController : ControllerBase
    {
        SettingService settingService;
        ILogger<CourseController> logger;
        CourseManageService courseManageService;
        ModelMapper modelMapper;
        public CourseController(SettingService _settingService, ILogger<CourseController> _logger, CourseManageService _courseManageService, PageTechsLMSDbContext dbContext)
        {
            settingService = _settingService;
            logger = _logger;
            courseManageService = _courseManageService;
            modelMapper = new ModelMapper(dbContext);
        }


        [HttpGet]
        public async Task<ApiMessage> GetCourseList(int page = 1, int size = 15)
        {
            var apiMsg = await ApiMessage.WrapAndTuple<Course>(async () =>
             {
                 var dataAndCount = await courseManageService.GetCoursesAsync(page, size);
                 return dataAndCount.ToTuple();
             });

            return apiMsg;
        }

        [HttpGet]
        public async Task<List<Lesson>> GetLessonList(Guid courseId, int page = 1, int size = 15)
        {
            return await courseManageService.GetLessonsAsync(courseId, page, size);
        }

        [HttpGet]
        public async Task<List<Section>> GetSectionList(Guid lessonId, int page = 1, int size = 15)
        {
            return await courseManageService.GetSectionsAsync(lessonId, page, size);
        }

        [HttpGet]
        public async Task<List<SectionItem>> GetSectionItemList(Guid sectionId, int page = 1, int size = 15)
        {
            return await courseManageService.GetSectionItemsAsync(sectionId, page, size);
        }

        [HttpGet]
        public async Task<ApiMessage> GetCourseAddOrUpdate(Guid? Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var modelType = await modelMapper.ModelToFormControl<DTOCourseInput>();
                ModelData modelData = null;
                if (Id != null)
                {
                    var data = await courseManageService.GetCourseDetailAsync(Id.Value);
                    modelData = await modelMapper.ModelToData(data);
                }
                return new { modelType, modelData };
            });
            return apiMsg;
        }
        [HttpGet]
        public async Task<ApiMessage> GetLessonAddOrUpdate(Guid? Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var modelType = await modelMapper.ModelToFormControl<DTOLessonInput>();
                ModelData modelData = null;
                if (Id != null)
                {
                    var data = await courseManageService.GetCourseDetailAsync(Id.Value);
                    modelData = await modelMapper.ModelToData(data);
                }
                return new { modelType, modelData };
            });
            return apiMsg;
        }
        [HttpGet]
        public async Task<ApiMessage> GetSectionAddOrUpdate(Guid? Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var modelType = await modelMapper.ModelToFormControl<DTOCourseInput>();
                ModelData modelData = null;
                if (Id != null)
                {
                    var data = await courseManageService.GetCourseDetailAsync(Id.Value);
                    modelData = await modelMapper.ModelToData(data);
                }
                return new { modelType, modelData };
            });
            return apiMsg;
        }
        [HttpGet]
        public async Task<ApiMessage> GetSectionItemAddOrUpdate(Guid? Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var modelType = await modelMapper.ModelToFormControl<DTOCourseInput>();
                ModelData modelData = null;
                if (Id != null)
                {
                    var data = await courseManageService.GetCourseDetailAsync(Id.Value);
                    modelData = await modelMapper.ModelToData(data);
                }
                return new { modelType, modelData };
            });
            return apiMsg;
        }

        [HttpPost]
        public async Task PostCourseAddOrUpdate(DTOCourseInput courseDto)
        {
            await courseManageService.AddOrUpdateCourseAsync(new Course
            {
                Id = courseDto.Id ?? Guid.Empty,
                Name = courseDto.Name,
                CategoryId = courseDto.CategoryId,
                Description = courseDto.Description,
                CommentNum = 0,
                Content = courseDto.Content,
                Cover = courseDto.Cover,
                CreateTime = DateTime.Now,
                Favorite = courseDto.Favorite,
                Icon = courseDto.Icon,
                IsHot = courseDto.IsHot,
                Level = courseDto.Level,
                Like = courseDto.Like,
                Price = courseDto.Price,
                StartNum = courseDto.StartNum,
                Video = courseDto.Video,
                Tags = courseDto.Tags.Select(x => new Tags { Name = x }).ToList(),
                ViewNumb = courseDto.ViewNumb

            });
        }

        [HttpPost]
        public async Task PostLessonAddOrUpdate(Lesson lesson)
        {
            await courseManageService.AddOrUpdateLessonAsync(lesson);
        }

        [HttpPost]
        public async Task PostSectionAddOrUpdate(Section section)
        {
            await courseManageService.AddOrUpdateSectionAsync(section);
        }

        [HttpPost]
        public async Task PostSectionItemAddOrUpdate(SectionItem sectionItem)
        {
            await courseManageService.AddOrUpdateSectionItemAsync(sectionItem);
        }

        [HttpPost]
        public async Task DeleteCourse(Guid Id)
        {
            await courseManageService.DeleteCourseAsync(Id);
        }
        [HttpPost]
        public async Task DeleteLesson(Guid Id)
        {
            await courseManageService.DeleteLessonAsync(Id);
        }

        [HttpPost]
        public async Task DeleteSection(Guid Id)
        {
            await courseManageService.DeleteSectionAsync(Id);
        }

        [HttpPost]
        public async Task DeleteSectionItem(Guid Id)
        {
            await courseManageService.DeleteSectionItemAsync(Id);
        }
    }
}
