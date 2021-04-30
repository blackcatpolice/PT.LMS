
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Abstractions;
using Microsoft.AspNetCore.Builder;
using System.Web;
using Microsoft.AspNetCore.Identity;
using PageTechsLMS.DataCore.Members;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Pagetechs.Framework.Wx.WebApp;
using System.Security.Claims;
using Pagetechs.Framework.Wx.Pay.WxPayAPI;
using Pagetechs.Framework.Wx.Pay;
using PageTechsLMS.Service.Courses;
using PageTechsLMS.Service.Orders;
using System.IO;

namespace PageTechsLMS.Service.Wx
{
    public static class WxPayMidleware
    {
        public static IApplicationBuilder UseWxPayProccessor(this IApplicationBuilder app)
        {
            app.Map("/WxNotifyUrl", appBuilder =>
            {
                appBuilder.Run(async ctx =>
                {
                    //var wxService = appBuilder.ApplicationServices.GetService<WxService>();
                    var logger = ctx.RequestServices.GetService<ILogger<WxService>>();
                    var coursePayService = ctx.RequestServices.GetService<CouseOrderService>();
                    try
                    {
                        WxPayData notifyData;
                        //var reqData = await new StreamReader(ctx.Request.Body).ReadToEndAsync();
                        //logger.LogInformation(reqData);
                        //ctx.Request.Body.Position = 0;
                        var responseStr = WeAppPayApi.ProcessNotify(ctx.Request.Body, out notifyData);

                        if (notifyData != null)
                        {

                            logger.LogInformation("notify data ");
                            logger.LogInformation(notifyData.ToJson());

                            var out_trade_no = notifyData.GetValue("out_trade_no").ToString();
                            if (!string.IsNullOrEmpty(out_trade_no))
                            {
                                await coursePayService.NotifyOrder(out_trade_no);
                            }
                            else
                            {
                                logger.LogError("微信服务器数据处理失败。");
                                await ctx.Response.WriteAsync("Faulth");
                            }
                        }

                        await ctx.Response.WriteAsync(responseStr);

                    }
                    catch (Exception exc)
                    {
                        logger.LogError(exc, exc.Message);
                    }
                });
            });

            app.Map("/CheckOrderStatus", appBuilder =>
            {
                appBuilder.Run(async ctx =>
                {
                    var orderId = ctx.Request.Query["orderId"];
                    var courseOrderService = ctx.RequestServices.GetService<CouseOrderService>();
                    var order = await courseOrderService.GetOrder(Guid.Parse(orderId));
                    if (order.Status == DataCore.Orders.OrderStatus.Payed)
                    {
                        ctx.Response.ContentType = "application/json";
                        await ctx.Response.WriteAsync("{\"success\":true}", Encoding.UTF8); 
                    }
                });
            });

            return app;
        }
    }
}
