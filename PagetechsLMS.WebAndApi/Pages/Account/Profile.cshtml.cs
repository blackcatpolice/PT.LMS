using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PageTechsLMS.DataCore.Courses;
using PageTechsLMS.DataCore.Members;
using PageTechsLMS.DataCore.Orders;
using PageTechsLMS.Service.Courses;
using PageTechsLMS.Service.Filebase;
using PageTechsLMS.Service.Member;
using PageTechsLMS.Service.Orders;

namespace PagetechsLMS.WebAndApi.Pages.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        UserManager<MemberAccount> userManager;
        MemberInfoSerivce memberInfoSerivce;
        FilebaseService filebaseService;
        public ProfileModel(
            UserManager<MemberAccount> _userManager, MemberInfoSerivce _memberInfoSerivce, FilebaseService _filebaseService)
        {
            userManager = _userManager;
            memberInfoSerivce = _memberInfoSerivce;
            filebaseService = _filebaseService;
        }

        public MemberAccount MemberAccount { get; set; }

        public MemberInfo MemberInfo { get; set; }

        public async Task<IActionResult> OnGet()
        {
            MemberAccount = await userManager.GetUserAsync(User);
            MemberInfo = await memberInfoSerivce.GetUserInfo(MemberAccount.Id);
            return Page();
        }

        public async Task<IActionResult> OnPost(ProfileInput profile)
        {
            var member = await userManager.GetUserAsync(User);

            var path = await filebaseService.SaveFile(Request.Form.Files[0]);

            await memberInfoSerivce.UpdateUserInfo(new MemberInfo
            {
                MemberId = member.Id,
                Avatart = path,
                Name = profile.Name
            });

            return Page();
        }

    }

    public class ProfileInput
    {
        public string Name { get; set; }

        public string Avatar { get; set; }
    }
}
