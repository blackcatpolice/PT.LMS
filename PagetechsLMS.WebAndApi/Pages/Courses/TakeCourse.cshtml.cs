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
using PageTechsLMS.Service.Pay;

namespace PagetechsLMS.WebAndApi.Pages.Courses
{
    public class TakeCourseModel : PageModel
    {
        CourseExploreService exploreService;
        CoursePayService coursePayService;
        CouseOrderService couseOrderService;
        PayService payService;
        Course CourseData { get; set; }
        public TakeCourseModel(CourseExploreService _exploreService, CoursePayService _coursePayService, CouseOrderService
            _couseOrderService, PayService _payService)
        {
            exploreService = _exploreService;
            coursePayService = _coursePayService;
            couseOrderService = _couseOrderService;
            payService = _payService;
        }
        public async Task OnGet(Guid courseId)
        {
            CourseData = await exploreService.GetCourseAsync(courseId);

            await couseOrderService.GetOrder(courseId);
        }

        public async Task OnPost(TaskCourseInput courseInput)
        {
            var memberId = User.Claims.First(x => x.Type == "sub").Value;
            var course = await exploreService.GetCourseAsync(courseInput.CourseId);
            var orderId = Guid.NewGuid();
            await couseOrderService.CreateOrder(new CourseOrder
            {
                Id = orderId,
                CourseId = courseInput.CourseId,
                CreateTime = DateTime.Now,
                MemberId = memberId,
                Fee = (course.Price * 100).ToString(),
                Name = "Order - " + course.Name,
                Status = OrderStatus.Create,
                Price = course.Price,
                Desc = course.Description,
                OrderType = OrderType.Course,
                PayChannel = PayChannel.WeChatNative
            });
            Redirect("/Pay/WxNativePay/" + orderId);
        }
    }

    public class TaskCourseInput
    {
        public Guid CourseId { get; set; }
    }
}
