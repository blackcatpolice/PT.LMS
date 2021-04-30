using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PageTechsLMS.DataCore.File;
using PageTechsLMS.DataCore.Setting.File;
using PageTechsLMS.Service.Filebase;
using PageTechsLMS.Service.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PageTechsLMS.Admin.Controllers.Filebase
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class FilebaseController : ControllerBase
    {
        FilebaseService filebaseService;
        SettingService settingService;

        public FilebaseController(FilebaseService _filebaseService, SettingService _settingService)
        {
            filebaseService = _filebaseService;
            settingService = _settingService;
        }

        [Route("GetSetting")]
        [HttpGet]
        public Dictionary<string, string> GetSetting()
        {
            var dir = new Dictionary<string, string>();
            dir.Add("use", settingService.File.Use.ToString());
            dir.Add("url", settingService.File.Url);
            return dir;
        }


        [Route("UploadFile")]
        [HttpPost]
        public async Task<string> UploadFile(IFormFile formFile)
        {
            return await filebaseService.SaveFile(formFile);
        }

        [Route("GetFile/{Id}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetFile(string Id)
        {
            if (Id.Contains("."))
            {
                Id = Path.GetFileNameWithoutExtension("Id");
            }
            var filebytes = await filebaseService.GetFile(Guid.Parse(Id));
            return File(filebytes, "application/octet-stream");
        }

        [Route("UploadFileThirdParty")]
        [HttpPost]
        public async Task<IActionResult> UploadFileThirdParty(FilebaseInfo fileInfoVM)
        {
            await filebaseService.SaveTridFile(fileInfoVM);
            return Ok();
        }

        [Route("GenerateQiniu")]
        [HttpGet]
        public string GenerateQiniu()
        {
            return filebaseService.GenerateQiniu();
        }


    }
}
