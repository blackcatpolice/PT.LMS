using PageTechsLMS.DataCore.Orders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Orders
{
    public class CourseOrder : IOrderBase
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public double Price { get; set; }
        public string OutTradeNo { get; set; }
        public OrderStatus Status { get; set; }
        public string Name { get; set; }
        public string Fee { get; set; }
        public string TradeId { get; set; }
        public string MemberId { get; set; }
        public string Channel { get; set; }
        public PayChannel PayChannel { get; set; }
        public OrderType OrderType { get; set; }
        public string Desc { get; set; }
        public string Tag { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
