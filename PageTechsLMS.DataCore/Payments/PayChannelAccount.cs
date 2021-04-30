using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Payments
{
    public class PayChannelAccount
    {
        public Guid Id { get; set; }
        public string ChannelName { get; set; }

        public double ChannelTotal { get; set; }
    }
}
