using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PageTechsLMS.DataCore.Orders;
using PageTechsLMS.Service.Orders;
using PageTechsLMS.Service.Pay;

namespace PagetechsLMS.WebAndApi.Pages.Pay
{
    public class PayingModel : PageModel
    {
        CouseOrderService couseOrderService;
        PayService payService;

        public CourseOrder Order { get; set; }
        public string PayData { get; set; }

        public PayingModel(CouseOrderService _couseOrderService, PayService _payService)
        {
            couseOrderService = _couseOrderService;
            payService = _payService;
        }
        public async Task OnGet(Guid orderId)
        {
            Order = await couseOrderService.GetOrder(orderId);
            var openId = User.Claims.FirstOrDefault(x => x.Type == "wxopenid").Value;
            var notifyUrl = Request.Host + "/WxNotifyUrl";
            var payOrderData = await payService.PayOrder(openId, Order, notifyUrl);
            PayData = payOrderData.Item1;
            await couseOrderService.UpdateOrder(payOrderData.Item2);
        }
    }
}
