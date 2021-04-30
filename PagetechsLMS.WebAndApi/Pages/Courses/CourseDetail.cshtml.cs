using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PageTechsLMS.DataCore.Courses;
using PageTechsLMS.DataCore.Orders;
using PageTechsLMS.Service.Courses;
using PageTechsLMS.Service.Orders;

namespace PagetechsLMS.WebAndApi.Pages.Courses
{
    public class CourseDetailModel : PageModel
    {
        CourseExploreService courseExploreService;
        CouseOrderService couseOrderService;
        public CourseDetailModel(CourseExploreService _courseExploreService, CouseOrderService _couseOrderService)
        {
            courseExploreService = _courseExploreService;
            couseOrderService = _couseOrderService;
        }
        public Course CourseData { get; set; }
        public CourseOrder CourseOrder { get; set; }
        public async Task<ActionResult> OnGet(Guid id)
        {
            string userId = string.Empty;
            if (User.Identity.IsAuthenticated)
            {
                userId = User.Claims.FirstOrDefault(x => x.Type == "sub").Value;
            }
            CourseOrder = await couseOrderService.GetMyOrder(id, userId);
            if (CourseOrder == null || CourseOrder.Status != OrderStatus.Payed)
            {
                return Redirect("/Courses/TrailerCourse?id=" + id.ToString());
            }

            CourseData = await courseExploreService.GetCourseAsync(id);

            return Page();
        }
    }
}
