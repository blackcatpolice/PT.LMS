using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PageTechsLMS.DataCore.Courses;
using PageTechsLMS.DataCore.Members;
using PageTechsLMS.DataCore.Orders;
using PageTechsLMS.Service.Courses;
using PageTechsLMS.Service.Orders;

namespace PagetechsLMS.WebAndApi.Pages.Account
{
    public class DashboardModel : PageModel
    {

        CouseOrderService couseOrderService;
        CourseLearnService courseLearnService;
        UserManager<MemberAccount> userManager;
        public DashboardModel(CouseOrderService _couseOrderService,
            CourseLearnService _courseLearnService,
            UserManager<MemberAccount> _userManager)
        {
            couseOrderService = _couseOrderService;
            courseLearnService = _courseLearnService;
            userManager = _userManager;
        }
        public List<CourseOrder> MyCourseOrder { get; set; }
        public List<Course> MyCourse { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var userModel = await userManager.GetUserAsync(User);
            var courseListData = await courseLearnService.GetMyCourseList(userModel.Id);
            MyCourseOrder = await couseOrderService.GetMyOrders(userModel.Id);
            MyCourse = courseListData.Item1;
            return Page();
        }
        public async Task<IActionResult> OnGetCourseList(int page = 1, int size = 15)
        {
            var userModel = await userManager.GetUserAsync(User);
            var courseListData = await courseLearnService.GetMyCourseList(userModel.Id, page, size);
            MyCourse = courseListData.Item1;
            return Page();
        }

        public async Task<IActionResult> OnGetCourseOrderList(int page = 1, int size = 15)
        {
            var userModel = await userManager.GetUserAsync(User);
            MyCourseOrder = await couseOrderService.GetMyOrders(userModel.Id, page, size);
            return Page();
        }
    }
}
