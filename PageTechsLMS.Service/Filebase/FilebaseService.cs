using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pagetechs.Framework.Qinius;
using PageTechsLMS.DataCore.DbContexts;
using PageTechsLMS.DataCore.File;
using PageTechsLMS.DataCore.Setting.File;
using PageTechsLMS.Service.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.Service.Filebase
{
    public class FilebaseService
    {
        SettingService settingService;
        PageTechsLMSDbContext dbContext;
        public FilebaseService(SettingService _settingService, PageTechsLMSDbContext _dbContext)
        {
            settingService = _settingService;
            dbContext = _dbContext;
        }

        public async Task<byte[]> GetFile(Guid fileId)
        {
            var fileInfo = await dbContext.FilebaseInfo.FirstOrDefaultAsync(x => x.Id == fileId);
            return await File.ReadAllBytesAsync(fileInfo.PhysicPath);
        }
        public async Task<string> SaveFile(IFormFile formFile)
        {
            var fileId = Guid.NewGuid();
            var basePathDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads");
            var fileDir = Path.Combine(basePathDir, "");
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }
            var ext = Path.GetExtension(formFile.FileName);

            var filePath = Path.Combine(fileDir, fileId.ToString() + ext);

            var fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
            await formFile.CopyToAsync(fileStream);
            fileStream.Close();

            var fileInfo = new FilebaseInfo()
            {
                Id = fileId,
                CreateTime = DateTime.Now,
                Ext = ext,
                Name = formFile.Name,
                PhysicPath = filePath,
                RelativePath = Path.Combine("/", fileId.ToString() + ext)
            };
            dbContext.Add(fileInfo);
            await dbContext.SaveChangesAsync();

            return Path.Combine("/Uploads/", fileId.ToString() + ext);
        }

        public async Task SaveTridFile(FilebaseInfo fileInfoVM)
        {
            var fileInfo = new FilebaseInfo()
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                Ext = fileInfoVM.Ext,
                Name = fileInfoVM.Name,
                PhysicPath = fileInfoVM.PhysicPath,
                RelativePath = fileInfoVM.PhysicPath
            };
            dbContext.Add(fileInfo);
            await dbContext.SaveChangesAsync();
        }

        public string GenerateQiniu()
        {
            var qiniu = new QiniuTool(settingService.File.AppKey, settingService.File.AppSecret, settingService.File.Bucket, settingService.File.Zone);
            return qiniu.GenerateToken();
        }
    }
}
