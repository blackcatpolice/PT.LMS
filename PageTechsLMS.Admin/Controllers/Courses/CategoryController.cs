using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pagetechs.Framework.Dtos;
using Pagetechs.Framework.Dtos.ModolHelper;
using PageTechsLMS.Admin.Dtos;
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
    public class CategoryController : ControllerBase
    {
        SettingService settingService;
        ILogger<CourseController> logger;
        CourseManageService courseManageService;
        ModelMapper modelMapper;
        public CategoryController(SettingService _settingService, ILogger<CourseController> _logger, CourseManageService _courseManageService, PageTechsLMSDbContext dbContext)
        {
            settingService = _settingService;
            logger = _logger;
            courseManageService = _courseManageService;
            modelMapper = new ModelMapper(dbContext);
        }

        [HttpGet]
        public async Task<ApiMessage> GetCategoryList(int page = 1, int limit = 15)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                return await courseManageService.GetCategoryAsync(page, limit);
            });
            return apiMsg;
        }

        [HttpGet]
        public async Task<ApiMessage> GetCategoryAddOrUpdate(Guid? Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var modelType = await modelMapper.ModelToFormControl<DTOCategoryInput>();
                ModelData modelData = null;
                if (Id != null)
                {
                    var data = await courseManageService.GetCategoryDetailAsync(Id.Value);
                    modelData = await modelMapper.ModelToData(data);
                }
                return new { modelType, modelData };
            });
            return apiMsg;
        }
        [HttpPost]
        public async Task<ApiMessage> PostCategoryAddOrUpdate(DTOCategoryInput dTOInput)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await courseManageService.AddOrUpdateCategoryAsync(new DataCore.Courses.Category
                {
                    Id = dTOInput.Id ?? Guid.Empty,
                    Cover = dTOInput.Cover,
                    Description = dTOInput.Description,
                    Name = dTOInput.Name
                });
            });
            return apiMsg;
        }
        [HttpPost]
        public async Task<ApiMessage> DeleteCategory(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await courseManageService.DeleteCategoryAsync(Id);
            });
            return apiMsg;
        }
    }
}
