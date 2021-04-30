using PageTechsLMS.DataCore.DbContexts;
using PageTechsLMS.DataCore.Orders;
using PageTechsLMS.DataCore.Orders.Interfaces;
using PageTechsLMS.Service.Wx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.Service.Pay
{
    public class PayService
    {
        PageTechsLMSDbContext dbContext;
        WxPayService wxPayService;
        public PayService(PageTechsLMSDbContext _dbContext, WxPayService _wxPayService)
        {
            dbContext = _dbContext;
            wxPayService = _wxPayService;
        }

        public async Task<(string, CourseOrder)> PayOrder(string openId, IOrderBase order, string notifyurl)
        {
            (string, CourseOrder) result;

            switch (order.PayChannel)
            {
                case PayChannel.WeChat:
                default:
                    result = await Task.Run(() =>
                    {
                        var uniferOrderData = wxPayService.CreateOrder(openId, order, notifyurl);
                        order.OutTradeNo = uniferOrderData.Item2;
                        return (uniferOrderData.Item1, order as CourseOrder);
                    });
                    break;
            }
            return result;
        }

        public async Task<(Stream, CourseOrder)> PayNativeOrder(string openId, IOrderBase order, string notifyurl)
        {
            var (picStream, outTrade) = wxPayService.NativeCreateOrder(openId, order, notifyurl);
            order.OutTradeNo = outTrade;
            return await Task.Run(() => (picStream, order as CourseOrder));
        }
    }
}
