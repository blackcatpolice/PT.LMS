using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PageTechsLMS.DataCore.Courses;
using PageTechsLMS.DataCore.MemberCourse;
using PageTechsLMS.DataCore.Orders;
using PageTechsLMS.Service.Courses;
using PageTechsLMS.Service.Orders;

namespace PagetechsLMS.WebAndApi.Pages.Courses
{
    public class TrailerCourseModel : PageModel
    {
        CourseExploreService courseExploreService;
        CouseOrderService couseOrderService;
        public TrailerCourseModel(CourseExploreService _courseExploreService, CouseOrderService _couseOrderService)
        {
            courseExploreService = _courseExploreService;
            couseOrderService = _couseOrderService;
        }

        public Course CourseDetail { get; set; }

        public CourseOrder CourseOrder { get; set; }

        public MemberFeedbackLog FeedbackList { get; set; }

        public List<Course> RecommandCourse { get; set; }



        public async Task<ActionResult> OnGet(Guid id)
        {
            string userId = string.Empty;

            if (User.Identity.IsAuthenticated)
            {
                userId = User.Claims.FirstOrDefault(x => x.Type == "sub").Value;
            }

            CourseDetail = await courseExploreService.GetCourseAsync(id);
            CourseOrder = await couseOrderService.GetMyOrder(id, userId);

            if (CourseOrder != null && CourseOrder.Status == OrderStatus.Payed)
            {
                return Redirect("/Courses/CourseDetail?id=" + id.ToString());
            }
            return Page();
        }
    }
}
