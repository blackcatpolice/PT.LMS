using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Orders.Interfaces
{
    public interface IOrderBase
    {
        public string Name { get; set; }
        public string Fee { get; set; }
        public string TradeId { get; set; }
        public string OutTradeNo { get; set; }
        public DateTime CreateTime { get; set; }
        public string MemberId { get; set; }
        public PayChannel PayChannel { get; set; }
        public OrderStatus Status { get; set; }
        public OrderType OrderType { get; set; }
        public string Desc { get; set; }
        public string Tag { get; set; }
        public Guid Id { get; set; }
    }
}
