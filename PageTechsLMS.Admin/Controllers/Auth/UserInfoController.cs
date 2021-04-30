using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pagetechs.Framework.Dtos;
using Pagetechs.Framework.Dtos.ModolHelper;
using PageTechsLMS.Admin.DbContexts;
using PageTechsLMS.Admin.Dtos.UserInfo;
using PageTechsLMS.DataCore.AdminUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PageTechsLMS.Admin.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class UserInfoController : ControllerBase
    {
        PTAuthenticationDbContext dbContext;
        UserManager<PTUserEntity> userManager;
        ModelMapper modelMapper;
        public UserInfoController(PTAuthenticationDbContext _dbContext, UserManager<PTUserEntity> _userManager)
        {
            dbContext = _dbContext;
            userManager = _userManager;
            modelMapper = new ModelMapper(dbContext);
        }

        [HttpGet]
        public async Task<object> GetUserInfo()
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "sub" || x.Type.Contains("nameidentifier")).Value;
                var userEntity = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
                return new
                {
                    avatar = "",
                    name = userEntity.UserName,
                    phone = userEntity.PhoneNumber,
                    email = userEntity.Email
                };
            });
            return apiMsg;
        }


        [HttpGet]
        public async Task<IList<string>> GetRole()
        {
            PTUserEntity userEntity = await getUser();
            var resData = await userManager.GetRolesAsync(userEntity);
            return resData;
        }

        private async Task<PTUserEntity> getUser()
        {
            var subId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var userEntity = await userManager.FindByIdAsync(subId);
            return userEntity;
        }

        [HttpGet]
        public async Task<ApiMessage> GetChangePasswordData()
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var modelType = await modelMapper.ModelToFormControl<ChangePasswordDTO>();

                return new { modelType };
            });
            return apiMsg;
        }

        [HttpPost]
        public async Task<ApiMessage> ChangePassword(ChangePasswordDTO passwordDTO)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                var currentUser = await userManager.GetUserAsync(User);
                var result = await userManager.ChangePasswordAsync(currentUser, passwordDTO.OldPassword, passwordDTO.NewPassword);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(",", result.Errors));
                }
                return result;
            });
            return apiMsg;
        }
    }
}
