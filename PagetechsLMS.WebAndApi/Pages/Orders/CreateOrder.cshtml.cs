using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PageTechsLMS.DataCore.Courses;
using PageTechsLMS.DataCore.Members;
using PageTechsLMS.DataCore.Orders;
using PageTechsLMS.Service.Courses;
using PageTechsLMS.Service.Orders;
using PageTechsLMS.Service.Pay;
using PageTechsLMS.Service.Wx;

namespace PagetechsLMS.WebAndApi.Pages.Orders
{
    [Authorize]
    public class CreateOrderModel : PageModel
    {
        CoursePayService coursePayService;
        CouseOrderService courseOrderService;
        CourseExploreService exploreService;
        public string PayData { get; set; }
        public CourseOrder OrderData { get; set; }
        public Course CourseData { get; set; }
        ILogger<CreateOrderModel> logger;
        PayService payService;
        public CreateOrderModel(CourseExploreService _exploreService,
            CouseOrderService _courseOrderService,
            CoursePayService _coursePayService,
            ILogger<CreateOrderModel> _logger,
            PayService _payService)
        {
            logger = _logger;
            courseOrderService = _courseOrderService;
            coursePayService = _coursePayService;
            exploreService = _exploreService;
            payService = _payService;
        }
        public async Task OnGet(Guid courseId)
        {
            CourseData = await coursePayService.GetCourseAsync(courseId);
            var openId = User.Claims.First(x => x.Type == "wxopenid")?.Value;
            var notifyUrl = Request.Scheme + "://" + Request.Host + "/WxNotifyUrl";
            var order = await CreateOrder(courseId);
            if (Request.Headers["User-Agent"].Any(x => x.ToLower() == "micromessenger"))
            {
                var result = await payService.PayOrder(openId, order, notifyUrl);
                PayData = result.Item1;
            } 
        }

        public async Task<CourseOrder> CreateOrder(Guid courseId)
        {
            var memberId = User.Claims.First(x => x.Type == "sub").Value;
            var course = await exploreService.GetCourseAsync(courseId);
            var orderId = Guid.NewGuid();
            var orderModel = new CourseOrder
            {
                Id = orderId,
                CourseId = courseId,
                CreateTime = DateTime.Now,
                MemberId = memberId,
                Fee = (course.Price * 100).ToString(),
                Name = "Order - " + course.Name,
                Status = OrderStatus.Create,
                Price = course.Price,
                Desc = course.Description,
                OrderType = OrderType.Course,
                PayChannel = PayChannel.WeChatNative
            };
            await courseOrderService.CreateOrder(orderModel);
            return orderModel;
        }

        public async Task<IActionResult> OnPost(Guid courseId)
        {
            var memberId = User.Claims.First(x => x.Type == "sub").Value;
            var course = await exploreService.GetCourseAsync(courseId);
            var orderId = Guid.NewGuid();
            await courseOrderService.CreateOrder(new CourseOrder
            {
                Id = orderId,
                CourseId = courseId,
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


            return Redirect("/Pay/WxNativePay/?orderId=" + orderId);
        }
    }
}
