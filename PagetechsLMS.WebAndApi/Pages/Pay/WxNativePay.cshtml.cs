using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PageTechsLMS.DataCore.Orders;
using PageTechsLMS.Service.Orders;
using PageTechsLMS.Service.Pay;

namespace PagetechsLMS.WebAndApi.Pages.Pay
{
    [Authorize]
    public class WxNativePayModel : PageModel
    {
        CouseOrderService couseOrderService;
        PayService payService;

        public CourseOrder Order { get; set; }
        public string PayQrcode { get; set; }
        ILogger<WxNativePayModel> logger;

        public WxNativePayModel(CouseOrderService _couseOrderService, PayService _payService, ILogger<WxNativePayModel> _logger)
        {
            couseOrderService = _couseOrderService;
            payService = _payService;
            logger = _logger;
        }
        public async Task OnGet(Guid orderId)
        {
            Order = await couseOrderService.GetOrder(orderId);
            var openId = User.Claims.FirstOrDefault(x => x.Type == "wxopenid").Value;
            var notifyUrl = Request.Scheme + "://" + Request.Host + "/WxNotifyUrl";
            logger.LogInformation("notify url Host :" + notifyUrl);
            var payOrderData = await payService.PayNativeOrder(openId, Order, notifyUrl);

            byte[] bytes = new byte[payOrderData.Item1.Length];
            payOrderData.Item1.Position = 0;
            payOrderData.Item1.Read(bytes, 0, bytes.Length);
            PayQrcode = Convert.ToBase64String(bytes);
            await couseOrderService.UpdateOrder(payOrderData.Item2);
        }
    }
}
