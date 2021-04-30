using PageTechsLMS.DataCore.Members;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.DataCore.Payments
{
    public class Paylog
    {
        public Guid Id { get; set; }

        public string MemberId { get; set; }

        [ForeignKey("MemberId")]
        public MemberAccount Member { get; set; }

        public string OrderName { get; set; }

        public string OrderFee { get; set; }

        public string OrderChannel { get; set; }

        public string OrderType { get; set; }

     }
}
