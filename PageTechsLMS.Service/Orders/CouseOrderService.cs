using Microsoft.EntityFrameworkCore;
using PageTechsLMS.DataCore.DbContexts;
using PageTechsLMS.DataCore.Orders;
using PageTechsLMS.DataCore.Orders.Interfaces;
using PageTechsLMS.Service.Wx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.Service.Orders
{
    public class CouseOrderService
    {
        PageTechsLMSDbContext dbContext;
        WxPayService wxPayService;
        public CouseOrderService(PageTechsLMSDbContext _dbContext, WxPayService _wxPayService)
        {
            dbContext = _dbContext;
            wxPayService = _wxPayService;
        }

        public async Task CreateOrder(CourseOrder courseOrder)
        {
            dbContext.CourseOrder.Add(courseOrder);
            await dbContext.SaveChangesAsync();
        }

        public async Task NotifyOrder(string out_trade_no)
        {
            var courseOrder = await dbContext.CourseOrder.FirstOrDefaultAsync(x => x.OutTradeNo == out_trade_no);
            courseOrder.Status = OrderStatus.Payed;
            dbContext.Update(courseOrder);

            var courseLog = await dbContext.MemberCourseLearnLogs.FirstOrDefaultAsync(x => x.CourseId == courseOrder.CourseId && x.MemberId == courseOrder.MemberId);
            if (courseLog == null)
            {
                await dbContext.MemberCourseLearnLogs.AddAsync(new DataCore.MemberCourse.MemberCourseLearnLog
                {
                    CreateTime = DateTime.Now,
                    CourseId = courseOrder.CourseId,
                    MemberId = courseOrder.MemberId,
                    Id = Guid.NewGuid()
                });
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task<CourseOrder> GetOrder(Guid orderId)
        {
            return await dbContext.CourseOrder.FirstOrDefaultAsync(x => x.Id == orderId);
        }

        public async Task RefundOrder(string orderId)
        {

        }

        public async Task<CourseOrder> GetMyOrder(Guid id, string userId)
        {
            return await dbContext.CourseOrder.OrderByDescending(x => x.CreateTime).FirstOrDefaultAsync(x => x.MemberId == userId && x.CourseId == id);
        }

        public async Task UpdateOrder(CourseOrder order)
        {
            dbContext.CourseOrder.Update(order);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<CourseOrder>> GetMyOrders(string id, int page = 1, int size = 15)
        {
            return await dbContext.CourseOrder.Where(x => x.MemberId == id).Skip((page - 1) * size).Take(size).ToListAsync();
        }
    }
}
